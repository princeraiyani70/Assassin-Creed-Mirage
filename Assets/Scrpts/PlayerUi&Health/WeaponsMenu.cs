using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    public GameObject weaponsMenuUi;
    public bool weaponsMenuActive = false;
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon4StockUi;

    [Header("Rations")]
    public Inventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == false)
        {
            //Open weapon Menu
            weaponsMenuUi.SetActive(true);
            weaponsMenuActive = true;
            Time.timeScale = 0f;
            mainCamera.GetComponent<MainCameraController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == true)
        {
            //close weapon Menu
            weaponsMenuUi.SetActive(false);
            weaponsMenuActive = false;
            Time.timeScale = 1f;
            mainCamera.GetComponent<MainCameraController>().enabled = true;
        }
        WeaponsCheck();
    }

    void WeaponsCheck()
    {
        if (inventory.isWeapon1Picked == true)
        {
            weapon1.SetActive(true);
        }

        if (inventory.isWeapon2Picked == true)
        {
            weapon2.SetActive(true);
        }

        if (inventory.isWeapon3Picked == true)
        {
            weapon3.SetActive(true);
        }

        if (inventory.isWeapon4Picked == true)
        {
            weapon4.SetActive(true);
            weapon4StockUi.SetActive(true);
        }

    }

}
