using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void ThrowCoconut()
    {
        ThirdPersonMovement thirdPersonMovement = gameObject.GetComponentInParent<ThirdPersonMovement>();
        thirdPersonMovement.ThrowCoconut();
    }
}
