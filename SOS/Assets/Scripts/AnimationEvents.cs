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

    public void TurnOnWalk()
    {
        ThirdPersonMovement thirdPersonMovement = gameObject.GetComponentInParent<ThirdPersonMovement>();
        thirdPersonMovement.animator.SetBool("WasHit", false);
    }
}
