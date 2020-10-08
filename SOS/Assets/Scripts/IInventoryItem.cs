using System;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    bool HasOwner { get; set; }
    Sprite Image { get; }
    void OnPickup(GameObject player);
    void OnDrop();
    void OnUse();
}

public class InventoryEventArgs:EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
