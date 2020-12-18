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
    public Animator animator;
    private int coconutCount;

    void Start()
    {
        coconutCount = 0;
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
        bool continueSearch = true;
        for (int i = 0; i < itemCount && continueSearch; i++)
        {
            IInventoryItem item = gameObject.transform.GetChild(i).GetComponent<IInventoryItem>();
            
            
            if (item != null && item.Name != null && item.Name.Equals("Banana") && !isUsingBanana)
            {
                continueSearch = false;
                var banana = gameObject.transform.GetChild(i);
                banana.transform.parent = null;
                UseBanana();
            }
            else if(item != null && item.Name.Equals("Shield") )
            {
                continueSearch = false;
                var shield = gameObject.transform.GetChild(i);
                shield.transform.parent = null;
                UseShield();
            }
            else if (item != null && item.Name.Equals("Coconut") && hasClosePlayer)
            {
                if (coconutCount == 1)
                {
                    Debug.Log(gameObject.transform.GetChild(i).name + " Has coconut, and close player: " + transform.name);
                    continueSearch = false;
                    animator.SetBool("Jumping", false);
                    animator.SetBool("ThrowingCoconut", true);
                    coconutCount = -1;
                }
                coconutCount++;
            }
        }


        void UseBanana()
        {
            isUsingBanana = true;
            _navMeshAgent.speed = _navMeshAgent.speed * bananaSpeedMultiplier;
            remainingBananaTime = 5f;
            FindObjectOfType<PositionRandomizer>().SpawnBanana();
        }

        void UseShield()
        {
            if (!hasShield)
            {
                hasShield = true;
                Transform ai = this.transform;
                GameObject shieldBubble = ai.Find("ShieldBubble").gameObject;
                shieldBubble.SetActive(true);
            }
        }
    }

    public void ThrowCoconut()
    {
        animator.SetBool("ThrowingCoconut", false);
        animator.SetBool("IsWalking", true);
        int itemCount = gameObject.transform.childCount;
        bool continueSearch = true;
        for (int i = 0; i < itemCount && continueSearch; i++)
        {
            IInventoryItem item = gameObject.transform.GetChild(i).GetComponent<IInventoryItem>();
            gameObject.transform.GetChild(i).GetComponent<IInventoryItem>();
            if (item != null && item.Name.Equals("Coconut") && hasClosePlayer)
            {
                continueSearch = false;
                AIItemsInteraction aiMovement = this.GetComponentInParent<AIItemsInteraction>();
                aiMovement.coconutInventory.UseItem(item);
                
                animator.SetBool("Jumping", false);
                animator.SetBool("IsWalking", true);
            }
        }
    }
}
