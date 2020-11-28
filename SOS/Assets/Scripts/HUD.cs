using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory CoconutInventory;
    public Text CoconutCount;
    public Inventory PowerUpInventory;
    public Inventory WinItems;

    void Start()
    {
        CoconutInventory.ItemAdded += InventoryScript_ItemAdded;
        CoconutInventory.ItemRemoved += Inventory_ItemRemoved;

        PowerUpInventory.ItemAdded += InventoryScript_ItemAdded;
        PowerUpInventory.ItemRemoved += Inventory_ItemRemoved;

        WinItems.ItemAdded += InventoryScript_ItemAdded;
        WinItems.ItemRemoved += Inventory_ItemRemoved;
        
    }

    private void InitInventory()
    {
        Transform winItems = transform.Find("WinItems");
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
            inventoryPanel = transform.Find("WinItems");
        else if(e.Item.Name == "Coconut")
        {
            inventoryPanel = transform.Find("CoconutPanel");
            CoconutCount.text = "" + (Int32.Parse(CoconutCount.text) + 1);
        }
        else
            inventoryPanel = transform.Find("PowerUpPanel");
        if(e.Item.WinItem)
        {
            foreach (Transform slot in inventoryPanel)
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                if (!image.enabled)
                {
                    image.enabled = true;
                    image.sprite = e.Item.Image;
                    if(itemDragHandler != null)
                        itemDragHandler.Item = e.Item;
                    break;
                }
            }
        }
        else
        {
            if(e.ShouldUpdateSprite)
            {
                foreach (Transform slot in inventoryPanel)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                    image.enabled = true;
                    image.sprite = e.Item.Image;
                    if(itemDragHandler != null)
                        itemDragHandler.Item = e.Item;
                    break;
                }   
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
            inventoryPanel = transform.Find("WinItems");
        else if(e.Item.Name == "Coconut")
        {
            inventoryPanel = transform.Find("CoconutPanel");
            CoconutCount.text = "" + (Int32.Parse(CoconutCount.text) - 1);
        }
        else
            inventoryPanel = transform.Find("PowerUpPanel");
        if(e.Item.WinItem || e.ShouldUpdateSprite)
        {
            foreach (Transform slot in inventoryPanel)
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                image.enabled = false;
                image.sprite = null;

                if (itemDragHandler.Item != null && itemDragHandler.Item.Equals(e.Item))
                {
                    itemDragHandler.Item = null;
                    break;
                }
            }
        }
    }
}
