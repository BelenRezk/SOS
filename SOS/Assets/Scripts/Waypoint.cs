using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    private Image iconImg;
    private Text distanceText;
    public Transform player;
    public Transform target;
    private Transform targetInstance;
    public Camera cam;
    public string itemName;

    public float closeEnoughDistance;
    private bool tooClose = false;
    private bool targetSet;
    public bool isAbilityActive;
    private bool isItemBeingHeld;

    void Start()
    {
        iconImg = GetComponent<Image>();
        distanceText = GetComponentInChildren<Text>();
        targetSet = false;
        isAbilityActive = false;
        isItemBeingHeld = false;
        itemName = itemName + "(Clone)";
    }

    void Update()
    {
        if (isAbilityActive)
        {
            if (!targetSet && target != null)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
                InventoryItemBase targetScript = target.GetComponent<InventoryItemBase>();
                foreach (GameObject item in items)
                {
                    if(itemName.Equals(item.name))
                        targetInstance = item.transform;
                }
                targetSet = true;
            }
            if (targetInstance != null && targetInstance.gameObject.activeInHierarchy)
            {
                GetDistance();
                CheckOnScreen();
            }
        }
        else
        {
            ToggleUI(false);
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

        if (aux <= 0 || tooClose || isItemBeingHeld)
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
