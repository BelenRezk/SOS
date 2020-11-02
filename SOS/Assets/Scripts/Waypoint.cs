using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    private Image iconImg;
    private Text distanceText;
    public Transform player;
    public Transform target;
    private Transform targetInstance;
    public Camera cam;

    public float closeEnoughDistance;
    private bool tooClose = false;
    private bool targetSet = false;
    // Start is called before the first frame update
    void Start()
    {
        iconImg = GetComponent<Image>();
        distanceText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetSet && target != null)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            InventoryItemBase targetScript = target.GetComponent<InventoryItemBase>();
            foreach (GameObject item in items)
            {
                InventoryItemBase itemScript = item.GetComponent<InventoryItemBase>();
                if (targetScript != null && itemScript != null && targetScript.Equals(itemScript))
                    targetInstance = item.transform;
            }
            targetSet = true;
        }
        if (targetInstance != null)
        {
            GetDistance();
            CheckOnScreen();
        }
    }

    private void GetDistance()
    {
        float distance = Vector3.Distance(player.position, targetInstance.position);
        distanceText.text = distance.ToString("f1") + "m";
        if (distance < closeEnoughDistance)
        {
            ToggleUI(false);
            tooClose = true;
        }
        else
        {
            ToggleUI(true);
            tooClose = false;
        }
    }

    private void CheckOnScreen()
    {
        float aux = Vector3.Dot((targetInstance.position - cam.transform.position).normalized, cam.transform.forward);

        if (aux <= 0 || tooClose)
            ToggleUI(false);
        else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(targetInstance.position);
        }
    }

    private void ToggleUI(bool value)
    {
        iconImg.enabled = value;
        distanceText.enabled = value;
    }
}
