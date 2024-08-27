using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapon 1 Slot")]
    public GameObject weapon1;
    public bool isWeapon1Picked= false;
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
}
