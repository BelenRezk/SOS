using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

    Transform[] targets;
    int i = 0;
    public Inventory inventory;

    private float remainingBananaTime = 0f;
    private bool isUsingBanana = false;
    public float bananaSpeedMultiplier = 2.0f;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.LogError("Nav mesh not attached to" + gameObject.name);
        }
        else
        {
            GameObject [] collectibles = GameObject.FindGameObjectsWithTag("CollectiblesTag");
            GameObject [] boatPosition = GameObject.FindGameObjectsWithTag("BoatTag");
            targets = new Transform[collectibles.Length + 1];
            for(int x = 0; x < collectibles.Length ; x ++)
            {
                targets[x] = collectibles[x].transform;
            }
            targets[collectibles.Length] = boatPosition[0].transform;
            if(collectibles.Length < 1)
            {
                Debug.Log("None available positions");
            }
            else
            {
                _navMeshAgent.destination = targets[i].position;
            }
        }
    }

    void Update()
    {
        var dist = Vector3.Distance(targets[i].position,_navMeshAgent.transform.position);
        //TODO: Contemplar caso que un jugador agarre el objeto al que se está dirigiendo
        if(dist < 2)
        {
            if( i < targets.Length  - 1)
            {
                i++;
                _navMeshAgent.destination = targets[i].position;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();
        if (item != null && !item.HasOwner)
        {
            Debug.Log("ENTRE AL IF DE NOT NULL Y NO HAS OWNER");
            Debug.Log(inventory.gameObject.name);
            inventory.AddItem(item);
            item.HasOwner = true;
        }
        else if (item != null && item.HasOwner)
        {
            Debug.Log("AI ENTRE AL IF DE NOT NULL Y HAS OWNER");
            inventory.DropAllItems();
        }
    }



    public void UseBanana(float duration)
    {
        isUsingBanana = true;
        _navMeshAgent.speed = _navMeshAgent.speed * bananaSpeedMultiplier;
        remainingBananaTime = duration;
    }
}
