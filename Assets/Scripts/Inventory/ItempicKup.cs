using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItempicKup : MonoBehaviour
{
    public Item item;


    void Pickup()
    {
        //destroy
        Destroy(this.gameObject);
        //add item inventory
        InventoryManager.Instance.AddItem(item);
    }

    void OnMouseDown()
    {
        Pickup();
    }
}
