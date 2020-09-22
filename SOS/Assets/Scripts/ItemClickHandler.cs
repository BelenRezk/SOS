using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    public void OnItemClicked()
    {
        try
        {
            ItemDragHandler dragHandler =
            gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

            IInventoryItem item = dragHandler.Item;

            if (item != null)
                inventory.RemoveItem(item);
        }
        catch (NullReferenceException)
        {
            Debug.Log("No item");
        }
    }
}
