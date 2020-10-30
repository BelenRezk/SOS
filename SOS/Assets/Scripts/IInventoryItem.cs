using System;
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
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
