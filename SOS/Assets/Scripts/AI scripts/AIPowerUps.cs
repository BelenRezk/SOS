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
        if(isUsingBanana)
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
        int randomNumber = UnityEngine.Random.Range(1,10);
        if(randomNumber == 1)
        {
            UsePowerUp();
        }
    }

    private void UsePowerUp()
    {
        int itemCount = gameObject.transform.childCount;
        for (int i = 0; i < itemCount; i++)
        {
            IInventoryItem item = gameObject.transform.GetChild(i).GetComponent<IInventoryItem>();
            
            
            if (item != null && item.Name != null && item.Name.Equals("Banana") && !isUsingBanana)
            {
                var banana = gameObject.transform.GetChild(i);
                banana.transform.parent = null;
                UseBanana();
            }
            else if(item != null && item.Name.Equals("Shield") )
            {
                var shield = gameObject.transform.GetChild(i);
                shield.transform.parent = null;
                UseShield();
            }
            else if (item != null && item.Name.Equals("Coconut") && hasClosePlayer)
            {
                AIItemsInteraction aiMovement = this.GetComponentInParent<AIItemsInteraction>();
                aiMovement.inventory.UseItem(item);
            }
    }

    void UseBanana()
    {
        isUsingBanana = true;
        _navMeshAgent.speed = _navMeshAgent.speed * bananaSpeedMultiplier;
        remainingBananaTime = 5f;
    }

    void UseShield()
    {
        if (!hasShield)
        {
            hasShield = true;
        }
    }
}
}
