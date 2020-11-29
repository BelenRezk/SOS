using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{

    public void ThrowCoconut()
    {
        Debug.Log("CALLED THROW COCONUT");
        AIPowerUps powerups = gameObject.GetComponentInParent<AIPowerUps>();
        powerups.ThrowCoconut();
    }
}
