using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutMovement : MonoBehaviour
{
    public Transform objectDestination;
    public float throwSpeed = 2000f;

    void OnMouseDown(){
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = objectDestination.position;
        this.transform.parent = GameObject.Find("HeldObject").transform;
    }

    void OnMouseUp(){
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(objectDestination.forward * throwSpeed);
    }
}
