using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;

    private void Update()
    {
        if (!playerControl)
            return;


        if (onSurface)
        {
            fallingSpeed = -0.5f;

            playerOnLedge = Enviromentcheaker.CheclLedge(moveDir);
            if (playerOnLedge)
            {
                Debug.Log("Player On Ledge");
            }
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
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

        var movementDirection = MCC.flotRotation * movementInput;

        Cc.Move(movementDirection * movementSpeed * Time.deltaTime);
        if (movementAmount > 0)
        {
            requireRotation = Quaternion.LookRotation(movementDirection);
        }

        moveDir = movementDirection;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requireRotation, rotSpeed * Time.deltaTime);

        animator.SetFloat("movementValue", movementAmount, 0.2f, Time.deltaTime);
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
}
