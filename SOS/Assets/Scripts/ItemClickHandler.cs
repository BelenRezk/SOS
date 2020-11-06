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
            
        }
        catch (NullReferenceException)
        {
            Debug.Log("No item");
        }
    }
}
