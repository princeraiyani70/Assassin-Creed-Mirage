using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfgrenades;
    public int numberOfHealth;
    public int numberOfEnergy;

    [Header("Stocks")]
    public TextMeshProUGUI GrenadeStock1;
    public TextMeshProUGUI GrenadeStock2;
    public TextMeshProUGUI HealthStock;
    public TextMeshProUGUI EnergyStock;

    [Header("Health & Energy")]
    public GameObject healthSlot;
    public GameObject energySlot;


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

        GrenadeStock1.text = "" + numberOfgrenades;
        GrenadeStock2.text = "" + numberOfgrenades;
        HealthStock.text = "" + numberOfHealth;
        EnergyStock.text = "" + numberOfEnergy;

        if (numberOfHealth > 0)
        {
            healthSlot.SetActive(true);
        }
        else if (numberOfHealth <= 0)
        {
            healthSlot.SetActive(false);
        }

        if (numberOfEnergy > 0)
        {
            energySlot.SetActive(true);
        }
        else if (numberOfEnergy <= 0)
        {
            energySlot.SetActive(false);
        }

    }
}
