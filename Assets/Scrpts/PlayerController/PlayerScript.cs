using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Health & Energy")]
    public float playerHealth = 200f;
    public float presentHealth;
    public HealthBar healthBar;
    private float playerEnergy = 100f;
    public float presentEnergy;
    public EnergyBar energyBar;
    public GameObject DamageIndicator;

    [Header("Player Movement")]
    public float movementSpeed = 5f;
    public float rotSpeed = 600f;
    public MainCameraController MCC;
    public Enviromentcheaker Enviromentcheaker;
    Quaternion requireRotation;
    bool playerControl = true;

    [Header("Player Animator")]
    public Animator animator;

    [Header("Player Collision & Gravity")]
    public CharacterController Cc;
    public float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool onSurface;
    public bool playerOnLedge { get; set; }
    public LedgeInfo LedgeInfo { get; set; }
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;
    [SerializeField] Vector3 requiredMoveDir;
    Vector3 velocity;

    private void Awake()
    {
        presentHealth=playerHealth;
        presentEnergy=playerEnergy;

        healthBar.GiveFullHealth(presentHealth);
        energyBar.GiveFullEnergy(presentEnergy);
    }

    private void Update()
    {
        if (presentEnergy <= 0)
        {
            movementSpeed = 2f;

            if (!Input.GetButton("Horizontal") || !Input.GetButton("Vertical"))
            {
                animator.SetFloat("movementValue", 0f);
            }
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                animator.SetFloat("movementValue", 0.5f);
                StartCoroutine(setEnergy());
            }
        }

        if (presentEnergy >= 1)
        {
            movementSpeed = 5f;
        }

        if (animator.GetFloat("movementValue") >= 0.9999)
        {
            playerenergyDecrease(0.02f);
        }

        if (!playerControl)
            return;


        velocity = Vector3.zero;
        if (onSurface)
        {
            fallingSpeed = -0.5f;
            velocity = moveDir * movementSpeed;


            playerOnLedge = Enviromentcheaker.CheckLedge(moveDir, out LedgeInfo ledgeInfo);
            if (playerOnLedge)
            {
                LedgeInfo = ledgeInfo;
                playerLedgeMovement();
                Debug.Log("Player On Ledge");
            }
            animator.SetFloat("movementValue", velocity.magnitude / movementSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;

            velocity = transform.forward * movementSpeed / 2;
        }

        velocity.y = fallingSpeed;

        PlayerMovement();
        SurfaceCheck();
        animator.SetBool("onSurface", onSurface);
        Debug.Log("Player On Surface" + onSurface);
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

        requiredMoveDir = MCC.flotRotation * movementInput;

        Cc.Move(velocity * movementSpeed * Time.deltaTime);
        if (movementAmount > 0 && moveDir.magnitude > 0.2f)
        {
            requireRotation = Quaternion.LookRotation(moveDir);
        }

        moveDir = requiredMoveDir;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requireRotation, rotSpeed * Time.deltaTime);
    }

    void playerLedgeMovement()
    {
        float angle = Vector3.Angle(LedgeInfo.surfaceHit.normal, requiredMoveDir);

        if (angle < 90)
        {
            velocity = Vector3.zero;
            moveDir = Vector3.zero;
        }
    }

    void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }

    public void SetControl(bool hasControl)
    {
        this.playerControl = hasControl;
        Cc.enabled = hasControl;

        if (!hasControl)
        {
            animator.SetFloat("movementValue", 0f);
            requireRotation = transform.rotation;
        }
    }

    public bool HasPlayerControl
    {
        get => playerControl;
        set => playerControl = value;
    }

    public void playerHitDamage(float takeDamage)
    {
        presentHealth-=takeDamage;
        healthBar.SetHealth(presentHealth);
        StartCoroutine(ShowDamage());
        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }

    public void playerenergyDecrease(float energyDecrease)
    {
        presentEnergy-=energyDecrease;
        energyBar.SetEnergy(presentEnergy);
    }

    IEnumerator setEnergy()
    {
        presentEnergy = 0f;
        yield return new WaitForSeconds(5f);
        presentEnergy = 100f;
        energyBar.GiveFullEnergy(presentEnergy);
    }

    IEnumerator ShowDamage()
    {
        DamageIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        DamageIndicator.SetActive(false);
    }
}
