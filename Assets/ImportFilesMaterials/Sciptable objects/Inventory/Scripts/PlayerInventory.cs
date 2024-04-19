using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<GroundItem>();
        if (item)
        {
            if (inventory.AddItem(item.item))
                Destroy(col.gameObject);
        }
    }

    public void Save()
    {
        inventory.Save();
    }

    public void Load()
    {
        inventory.ClearInventory();
        inventory.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.ClearInventory();
    }
}