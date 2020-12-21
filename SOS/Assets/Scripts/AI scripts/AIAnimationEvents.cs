using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{
    private void Update() 
    {
        if(this.GetComponent<Animator>().GetBool("IsWalking") && this.GetComponent<Animator>().GetBool("ThrowingCoconut") && this.GetComponent<Animator>().GetBool("WasHit"))
        {
            this.GetComponent<Animator>().SetBool("ThrowingCoconut", false);
            this.GetComponent<Animator>().SetBool("WasHit", false);
        }    
    }

    public void ThrowCoconut()
    {
        this.GetComponent<Animator>().SetBool("ThrowingCoconut", false);
        this.GetComponent<Animator>().SetBool("IsWalking", true);
        AIPowerUps powerups = gameObject.GetComponentInParent<AIPowerUps>();
        powerups.animator.SetBool("ThrowingCoconut",false);
        powerups.animator.SetBool("IsWalking",true);
        powerups.ThrowCoconut();
    }

    public void TurnOnWalk()
    {
        AIPowerUps powerups = gameObject.GetComponentInParent<AIPowerUps>();
        powerups.animator.SetBool("WasHit", false);
        powerups.animator.SetBool("IsWalking", true);
        this.GetComponent<Animator>().SetBool("WasHit", false);
        this.GetComponent<Animator>().SetBool("IsWalking", true);
    }
}
