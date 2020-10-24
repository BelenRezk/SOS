using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public virtual bool HasOwner { get; set; }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }

    public virtual void OnPickup(GameObject player)
    {
        this.transform.parent = player.transform;
        gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("PickUpObject");
    }

    public virtual void OnDrop()
    {
        /*RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }*/

        Vector3 newPos = new Vector3(1, 1, 1);
        this.transform.position = this.transform.parent.position + newPos;
        HasOwner = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(true);
        this.transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
    }

    public abstract bool OnUse();
}
