using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    public Inventory WinItems;

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
        Inventory.PositionChanged += Inventory_PositionChanged;

        WinItems.ItemAdded += InventoryScript_ItemAdded;
        WinItems.ItemRemoved += Inventory_ItemRemoved;
        WinItems.PositionChanged += Inventory_PositionChanged;
        
    }

    private void InitInventory()
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        Transform winItems = transform.Find("WinItems");
        int position = 0;
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            if (position == 0)
                image.color = new Color32(255, 255, 255, 255);
            else
                image.color = new Color32(120, 120, 120, 140);
            position++;
        }
        foreach (Transform slot in winItems)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            if (position == 0)
                image.color = new Color32(255, 255, 255, 255);
            else
                image.color = new Color32(120, 120, 120, 140);
            position++;
        }
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
        {
            inventoryPanel = transform.Find("WinItems");
        }
        else
        {
            inventoryPanel = transform.Find("InventoryPanel");
        }
        Debug.Log("NOMBRE DE INV"+inventoryPanel.name);
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
        {
            inventoryPanel = transform.Find("WinItems");
        }
        else
        {
            inventoryPanel = transform.Find("InventoryPanel");
        }
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler.Item != null && itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }

    private void Inventory_PositionChanged(object sender, Tuple<int, int> positions)
    {
        int previousPosition = positions.Item1;
        int newPosition = positions.Item2;
        if (previousPosition != newPosition)
        {
            Transform inventoryPanel = transform.Find("InventoryPanel");
            int position = 0;
            foreach (Transform slot in inventoryPanel)
            {
                if (position == newPosition)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    image.color = new Color32(255, 255, 255, 255);
                }
                if (position == previousPosition)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    image.color = new Color32(120, 120, 120, 140);
                }
                position++;
            }
        }
    }
}
