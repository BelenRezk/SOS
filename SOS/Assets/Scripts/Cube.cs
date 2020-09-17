using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInventoryItem
{
    public GameObject player;
    public string Name
    {
        get
        {
            return "Oar";
        }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        this.transform.parent = player.transform;
        gameObject.SetActive(false);
    }
}
