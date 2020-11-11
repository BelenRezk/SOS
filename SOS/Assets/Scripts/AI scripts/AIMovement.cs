using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MovementBase
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

    Transform[] targets;
    int i = 0;

    private float remainingBananaTime = 0f;
    public float bananaSpeedMultiplier = 2.0f;
    private bool hasShield = false;
    public AudioClip getHitSound;
    public AudioClip shieldSound;
    [HideInInspector]
    public int coconutCount = 0;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("Nav mesh not attached to" + gameObject.name);
        }
        else
        {
            GameObject[] collectibles = GameObject.FindGameObjectsWithTag("CollectiblesTag");
            GameObject[] boatPosition = GameObject.FindGameObjectsWithTag("BoatTag");
            targets = new Transform[collectibles.Length + 1];
            for (int x = 0; x < collectibles.Length; x++)
            {
                targets[x] = collectibles[x].transform;
            }
            targets[collectibles.Length] = boatPosition[0].transform;
            if (collectibles.Length < 1)
            {
                Debug.Log("None available positions");
            }
            else
            {
                _navMeshAgent.destination = targets[i].position;
            }
        }
    }

    void Update()
    {
        var dist = Vector3.Distance(targets[i].position, _navMeshAgent.transform.position);
        if (dist < 1.5 || targets[i].parent != null)
        {
            if (i < targets.Length - 1)
            {
                i++;
                _navMeshAgent.destination = targets[i].position;
            }
        }
        if (interactionCooldownRemaining > 0)
            interactionCooldownRemaining -= Time.deltaTime;
    }

    public void ObjectInteraction(Collider other)
    {
        IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();
        if(interactionCooldownRemaining <= 0)
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
                if (!hasShield)
                {
                    PlayGetHitSound();
                    inventory.DropAllItems();
                    winItems.DropAllItems();
                    item.DestroyObject();
                }
                else
                {
                    PlayShieldSound();
                    hasShield = false;
                }
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

    public void CheckBananaUsage()
    {
        if (isUsingBanana)
        {
            if (remainingBananaTime > 0f)
            {
                remainingBananaTime -= Time.deltaTime;
            }
            else
            {
                isUsingBanana = false;
                _navMeshAgent.speed = _navMeshAgent.speed / bananaSpeedMultiplier;
            }
        }
    }

    public void UseBanana(float duration)
    {
        isUsingBanana = true;
        _navMeshAgent.speed = _navMeshAgent.speed * bananaSpeedMultiplier;
        remainingBananaTime = duration;
    }

    public bool UseShield()
    {
        if (!hasShield)
        {
            hasShield = true;
            return true;
        }
        else
            return false;
    }
}
