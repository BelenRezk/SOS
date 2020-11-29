﻿using System;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    bool HasOwner { get; set; }
    Sprite Image { get; }
    bool WinItem { get; }
    void OnPickup(GameObject player);
    void OnDrop();
    bool OnUse();
    void DestroyObject();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item, bool shouldUpdateSprite)
    {
        Item = item;
        ShouldUpdateSprite = shouldUpdateSprite;
    }
    public IInventoryItem Item;
    public bool ShouldUpdateSprite;
}
