using System;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup(GameObject player);
    void OnDrop();
}

public class InventoryEventArgs:EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
