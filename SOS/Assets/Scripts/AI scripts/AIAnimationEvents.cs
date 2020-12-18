using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{

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
