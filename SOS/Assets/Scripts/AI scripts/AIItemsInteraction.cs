using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIItemsInteraction : MovementBase
{
    public AudioClip getHitSound;
    public AudioClip shieldSound;
    [HideInInspector]
    public int coconutCount = 0;

    void Start(){}

    void Update(){
        if (interactionCooldownRemaining > 0)
            interactionCooldownRemaining -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        /*AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        if(aiMovement.interactionCooldownRemaining <= 0)
            aiMovement.ObjectInteraction(other);*/
        IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();           
        if(item != null && interactionCooldownRemaining <= 0)
        {
            interactionCooldownRemaining = interactionCooldown;
            
            if (item != null && !item.HasOwner)
            {
                if(item.WinItem)
                    winItems.AddItem(item);
                else
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
                    winItems.DropAllItems();
                }
                else
                {
                    PlayShieldSound();
                    obj.hasShield = false;
                    FindObjectOfType<PositionRandomizer>().SpawnShield();
                }
                item.DestroyObject();
                FindObjectOfType<PositionRandomizer>().SpawnCoconut();
            }
        }
    }

    private void PlayGetHitSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(getHitSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception)
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
        catch (Exception)
        {
            Debug.Log("No shield audio clip");
        }
    }
}
