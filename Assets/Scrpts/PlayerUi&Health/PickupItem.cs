using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item Info")]
    public int itemRadius;
    public string ItemTag;
    private GameObject ItemToPick;

    [Header("Player Info")]
    public Transform player;
    public Inventory inventory;


    private void Start()
    {
        ItemToPick=GameObject.FindWithTag(ItemTag);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < itemRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (ItemTag == "Sword")
                {
                    inventory.isWeapon1Picked = true;
                }
                else if (ItemTag == "Rifle")
                {
                    inventory.isWeapon2Picked = true;
                }
                else if (ItemTag == "Bazooka")
                {
                    inventory.isWeapon3Picked = true;
                }
                else if (ItemTag == "Grenade")
                {
                    inventory.isWeapon4Picked = true;
                }
                else if (ItemTag == "Health")
                {
                    Debug.Log(ItemTag + " Pickup");
                }
                else if (ItemTag == "Energy")
                {
                    Debug.Log(ItemTag + " Pickup");
                }

                ItemToPick.SetActive(false);
            }
        }
    }
}
