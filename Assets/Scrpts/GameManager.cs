using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfgrenades;
    public int numberOfHealth;
    public int numberOfEnergy;

    [Header("Ammo & Mag")]
    public Rifle rifle;
    public Bazooka bazooka;
    public TextMeshProUGUI RifleAmmoText;
    public TextMeshProUGUI RifleMagText;
    public TextMeshProUGUI BazookaAmmoText;
    public TextMeshProUGUI BazookaMagText;

    private void Update()
    {
        RifleAmmoText.text = "" + rifle.presentAmmunition;
        RifleMagText.text = "" + rifle.mag;

        BazookaAmmoText.text=""+bazooka.presentAmmunition;
        BazookaMagText.text = "" + bazooka.mag;
    }
}
