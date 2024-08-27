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
    public Animator anim;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && fistFightMode == false)
        {
            fistFightMode = true;
            IsRifleActive();
        }

        if (Input.GetKeyDown("1") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false &&isWeapon1Picked==true)
        {
            isWeapon1Active = true;
            IsRifleActive() ;
        }
        else if (Input.GetKeyDown("1") && isWeapon1Active == true)
        {
            isWeapon1Active = false;
            IsRifleActive();
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
}
