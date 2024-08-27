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
                    Debug.Log(ItemTag + " Pickup");
                }
                else if (ItemTag == "Rifle")
                {
                    Debug.Log(ItemTag + " Pickup");
                }
                else if (ItemTag == "Bazooka")
                {
                    Debug.Log(ItemTag + " Pickup");
                }
                else if (ItemTag == "Sword")
                {
                    Debug.Log(ItemTag + " Pickup");
                }
                else if (ItemTag == "Grenade")
                {
                    Debug.Log(ItemTag + " Pickup");
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
