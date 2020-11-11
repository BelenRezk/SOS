using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIItemsInteraction : MonoBehaviour
{
    public Inventory inventory;
    public AudioClip getHitSound;
    public AudioClip shieldSound;

    void Start(){}
    private void OnTriggerEnter(Collider other)
    {
        AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        if(aiMovement.interactionCooldownRemaining <= 0)
            aiMovement.ObjectInteraction(other);
        /*IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();
        if (item != null && !item.HasOwner)
        {
            inventory.AddItem(item);
            item.HasOwner = true;
        }
        else if (item != null && item.HasOwner)
        {
            AIPowerUps obj = gameObject.transform.GetComponent<AIPowerUps>();
            if (!obj.hasShield)
            {
                PlayGetHitSound();
                inventory.DropAllItems();
            }
            else
            {
                PlayShieldSound();
                obj.hasShield = false;
            }
        }*/
    }

    private void PlayGetHitSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(getHitSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception e)
        {
            Debug.Log("No get hit audio clip");
        }
    }

    private void PlayShieldSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(shieldSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception e)
        {
            Debug.Log("No shield audio clip");
        }
    }
}
