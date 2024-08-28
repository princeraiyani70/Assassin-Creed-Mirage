using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapon 1 Slot")]
    public GameObject weapon1;
    public bool isWeapon1Picked = false;
    public bool isWeapon1Active = false;
    public SingleMeleeAttack SMAS;

    public bool fistFightMode = false;

    [Header("Weapon 2 Slot")]
    public GameObject weapon2;
    public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;
    public Rifle rifle;

    [Header("Weapon 3 Slot")]
    public GameObject weapon3;
    public bool isWeapon3Picked = false;
    public bool isWeapon3Active = false;
    public Bazooka bazzoka;

    [Header("Weapon 4 Slot")]
    public GameObject weapon4;
    public bool isWeapon4Picked = false;
    public bool isWeapon4Active = false;
    public GrenadeThrow grenadethrower;

    [Header("Scripts")]
    public FirstFight fistFight;
    public PlayerScript playerScript;
    public GameManager GM;
    public Animator anim;

    [Header("Current Weapons")]
    public GameObject NoWeapon;
    public GameObject CurrentWeapon1;
    public GameObject CurrentWeapon2;
    public GameObject CurrentWeapon3;
    public GameObject CurrentWeapon4;

    private void Update()
    {
        if (isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && fistFightMode == false)
        {
            NoWeapon.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0) && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && fistFightMode == false)
        {
            fistFightMode = true;
            IsRifleActive();
        }

        if (Input.GetKeyDown("1") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon1Picked == true)
        {
            isWeapon1Active = true;
            IsRifleActive();
            CurrentWeapon1.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("1") && isWeapon1Active == true)
        {
            isWeapon1Active = false;
            IsRifleActive();
        }

        if (Input.GetKeyDown("2") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon2Picked == true)
        {
            isWeapon2Active = true;
            IsRifleActive();
            CurrentWeapon2.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("2") && isWeapon2Active == true)
        {
            isWeapon2Active = false;
            IsRifleActive();
            CurrentWeapon2.SetActive(false);
        }

        if (Input.GetKeyDown("3") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon3Picked == true)
        {
            isWeapon3Active = true;
            IsRifleActive();
            CurrentWeapon3.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("3") && isWeapon3Active == true)
        {
            isWeapon3Active = false;
            IsRifleActive();
            CurrentWeapon3.SetActive(false);
        }

        if (Input.GetKeyDown("4") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon4Picked == true)
        {
            isWeapon4Active = true;
            IsRifleActive();
            CurrentWeapon4.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("4") && isWeapon4Active == true)
        {
            isWeapon4Active = false;
            IsRifleActive();
            CurrentWeapon4.SetActive(false);
        }

        if (GM.numberOfgrenades <= 0 && isWeapon4Active == true)
        {
            weapon4.SetActive(false);
            isWeapon4Active = false;
            CurrentWeapon4.SetActive(false);
            IsRifleActive();
        }

        if (Input.GetKeyDown("5") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && GM.numberOfHealth > 0 && playerScript.presentHealth < 95)
        {
            StartCoroutine(IncreaseHealth());
        }

        if (Input.GetKeyDown("6") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && GM.numberOfEnergy > 0 && playerScript.presentEnergy < 95)
        {
            StartCoroutine(IncreaseEnergy());
        }

    }

    void IsRifleActive()
    {
        if (fistFightMode == true)
        {
            fistFight.GetComponent<FirstFight>().enabled = true;
        }

        if (isWeapon1Active == true)
        {
            StartCoroutine(Weapon1Go());
            SMAS.GetComponent<SingleMeleeAttack>().enabled = true;
            anim.SetBool("SingleHandAttackActive", true);
        }
        if (isWeapon1Active == false)
        {
            StartCoroutine(Weapon1Go());
            SMAS.GetComponent<SingleMeleeAttack>().enabled = false;
            anim.SetBool("SingleHandAttackActive", false);
        }

        if (isWeapon2Active == true)
        {
            StartCoroutine(Weapon2Go());
            rifle.GetComponent<Rifle>().enabled = true;
            anim.SetBool("RifleActive", true);
        }
        if (isWeapon2Active == false)
        {
            StartCoroutine(Weapon2Go());
            rifle.GetComponent<Rifle>().enabled = false;
            anim.SetBool("RifleActive", false);
        }

        if (isWeapon3Active == true)
        {
            StartCoroutine(Weapon3Go());
            bazzoka.GetComponent<Bazooka>().enabled = true;
            anim.SetBool("BazookaActive", true);
        }
        if (isWeapon3Active == false)
        {
            StartCoroutine(Weapon3Go());
            bazzoka.GetComponent<Bazooka>().enabled = false;
            anim.SetBool("BazookaActive", false);
        }


        if (isWeapon4Active == true)
        {
            StartCoroutine(Weapon4Go());
            grenadethrower.GetComponent<GrenadeThrow>().enabled = true;
        }
        if (isWeapon4Active == false)
        {
            StartCoroutine(Weapon4Go());
            grenadethrower.GetComponent<GrenadeThrow>().enabled = false;
        }
    }

    IEnumerator Weapon1Go()
    {
        if (!isWeapon1Active)
        {
            weapon1.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon1Active)
        {
            weapon1.SetActive(true);
        }
    }

    IEnumerator Weapon2Go()
    {
        if (!isWeapon2Active)
        {
            weapon2.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon2Active)
        {
            weapon2.SetActive(true);
        }
    }

    IEnumerator Weapon3Go()
    {
        if (!isWeapon3Active)
        {
            weapon3.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon3Active)
        {
            weapon3.SetActive(true);
        }
    }

    IEnumerator Weapon4Go()
    {
        if (!isWeapon4Active)
        {
            weapon4.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon4Active)
        {
            weapon4.SetActive(true);
        }
    }

    IEnumerator IncreaseHealth()
    {
        anim.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Drink", false);
        GM.numberOfHealth -= 1;
        playerScript.presentHealth = 200f;
        playerScript.healthBar.GiveFullHealth(200f);
    }

    IEnumerator IncreaseEnergy()
    {
        anim.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Drink", false);
        GM.numberOfEnergy -= 1;
        playerScript.presentEnergy = 100f;
        playerScript.energyBar.GiveFullEnergy(100f);
    }
}
