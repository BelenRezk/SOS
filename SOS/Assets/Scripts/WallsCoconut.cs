using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsCoconut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("coco") || other.gameObject.tag == "Item")
        {
            var coconut =other.gameObject.GetComponent<Rigidbody>();
            coconut.velocity = Vector3.zero;
            coconut.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name.Contains("coco") || other.gameObject.tag == "Item")
        {
            var coconut =other.gameObject.GetComponent<Rigidbody>();
            coconut.velocity = Vector3.zero;
            coconut.angularVelocity = Vector3.zero;
            coconut.useGravity = false;
        }
    }
}
