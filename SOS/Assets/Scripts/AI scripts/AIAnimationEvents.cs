using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{

    public void ThrowCoconut()
    {
        AIPowerUps powerups = gameObject.GetComponentInParent<AIPowerUps>();
        powerups.ThrowCoconut();
    }

    public void TurnOnWalk()
    {
        AIPowerUps powerups = gameObject.GetComponentInParent<AIPowerUps>();
        powerups.animator.SetBool("WasHit", false);
    }
}
