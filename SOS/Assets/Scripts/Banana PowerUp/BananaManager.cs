using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaManager : MonoBehaviour
{
    public float duration = 5f;
    public GameObject mainCharacter;

    public void Use(Transform user)
    {
        if (user == mainCharacter.transform)
        {
            ThirdPersonMovement thirdPersonMovement = mainCharacter.GetComponent<ThirdPersonMovement>();
            thirdPersonMovement.UseBanana(duration);
        }
    }
}
