using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPowerUps : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    private float remainingBananaTime = 0f;
    private bool isUsingBanana = false;
    public float bananaSpeedMultiplier = 2.0f;
    public bool hasShield = false;
    [HideInInspector]
    public bool hasClosePlayer = false;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("Nav mesh not attached to" + gameObject.name);
        }
    }
        
    void Update()
    {
        AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        if(aiMovement.isUsingBanana)
        {
            CheckBananaUsage();   
        }
        int randomNumber = UnityEngine.Random.Range(1,10);
        if(randomNumber == 1)
        {
            UsePowerUp();
        }
    }

    private void UsePowerUp()
    {
        int itemCount = gameObject.transform.childCount;
        int randomNumber = UnityEngine.Random.Range(0, itemCount);
        //for (int i = 0; i < itemCount; i++)
        //{
            IInventoryItem item = gameObject.transform.GetChild(randomNumber).GetComponent<IInventoryItem>();
            
            
            if (item != null && item.Name != null && item.Name.Equals("Banana") && !isUsingBanana)
            {
                var banana = gameObject.transform.GetChild(randomNumber);
                banana.transform.parent = null;
                UseBanana();
            }
            else if(item != null && item.Name.Equals("Shield") )
            {
                var shield = gameObject.transform.GetChild(randomNumber);
                shield.transform.parent = null;
                UseShield();
            }
            else if (item != null && item.Name.Equals("Coconut") && hasClosePlayer)
            {
                AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
                aiMovement.inventory.UseItem(item);
                //item.OnUse();
            }
        //}
    }

    private void UseBanana()
    {
        AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        aiMovement.UseBanana(5f);
        /*isUsingBanana = true;
        _navMeshAgent.speed = _navMeshAgent.speed * bananaSpeedMultiplier;
        remainingBananaTime = 5f;*/
    }

    private void CheckBananaUsage()
    {
        AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        aiMovement.CheckBananaUsage();
        /*if (isUsingBanana)
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
        }*/
    }

    private void UseShield()
    {
        AIMovement aiMovement = this.GetComponentInParent<AIMovement>();
        aiMovement.UseShield();
        if (!hasShield)
        {
            hasShield = true;
        }
    }
}
