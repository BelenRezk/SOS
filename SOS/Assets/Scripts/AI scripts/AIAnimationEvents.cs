using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEvents : MonoBehaviour
{

    public void ThrowCoconut()
    {
        ThirdPersonMovement thirdPersonMovement = gameObject.GetComponentInParent<ThirdPersonMovement>();
        thirdPersonMovement.ThrowCoconut();
    }
}
