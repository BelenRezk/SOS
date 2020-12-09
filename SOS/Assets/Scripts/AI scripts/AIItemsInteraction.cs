using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIItemsInteraction : MovementBase
{
    public AudioClip getHitSound;
    public AudioClip shieldSound;
    public AudioClip oldLadyLaugh;
    [HideInInspector]
    public int coconutCount = 0;
    public Animator animator;

    void Start()
    {
        abilityCooldownRemaining = 3f;
    }

    void Update(){
        if (interactionCooldownRemaining > 0){
            interactionCooldownRemaining -= Time.deltaTime;
        }
        if (abilityCooldownRemaining > 0){
            abilityCooldownRemaining -= Time.deltaTime;
        }
        if (abilityDurationRemaining > 0){
            abilityDurationRemaining -= Time.deltaTime;
        }
        if (currentInvincibility > 0)
            currentInvincibility -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("LavaPlane"))
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().Move(this.GetComponent<UnityEngine.AI.NavMeshAgent>().transform.forward*35.0f);
            coconutInventory.DropAllItems();
            powerUpInventory.DropAllItems();
            winItems.DropAllItems();
        }
        else
        {
            IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();           
            if(item != null && interactionCooldownRemaining <= 0)
            {
                interactionCooldownRemaining = interactionCooldown;
                
                if (item != null && !item.HasOwner)
                {
                    bool wasItemAdded = false;
                    if (item.WinItem)
                    {
                        if(winItems.currentNumberOfItems < winItems.SLOTS)
                        {
                            winItems.AddItem(item);
                            wasItemAdded = true;
                        }
                    }
                    else if(item.Name == "Coconut")
                    {
                        if(coconutInventory.currentNumberOfItems < coconutInventory.SLOTS)
                        {
                            coconutInventory.AddItem(item);
                            wasItemAdded = true;
                        }
                    }
                    else
                        if(powerUpInventory.currentNumberOfItems < powerUpInventory.SLOTS)
                        {
                            powerUpInventory.AddItem(item);
                            wasItemAdded = true;
                        }
                    if(wasItemAdded)
                        item.HasOwner = true;
                }
                else if (item != null && item.HasOwner)
                {
                    AIPowerUps obj = gameObject.transform.GetComponent<AIPowerUps>();
                    if(currentInvincibility <= 0)
                    {
                        if (!obj.hasShield)
                        {
                            animator.SetBool("IsWalking",false);
                            PlayGetHitSound();
                            coconutInventory.DropAllItems();
                            powerUpInventory.DropAllItems();
                            winItems.DropAllItems();
                        }
                        else
                        {
                            PlayShieldSound();
                            obj.hasShield = false;
                            FindObjectOfType<PositionRandomizer>().SpawnShield();
                        }
                    }
                    else
                        AudioSource.PlayClipAtPoint(oldLadyLaugh, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
                    item.DestroyObject();
                    FindObjectOfType<PositionRandomizer>().SpawnCoconut();
                }
            }
        }
    }

    private void PlayGetHitSound()
    {
        try
        {
            animator.SetBool("WasHit", true);
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