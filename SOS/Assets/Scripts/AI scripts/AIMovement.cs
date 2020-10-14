using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    Transform[] targets;
    int i = 0;
    public Inventory inventory;

    void Start()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        if(dist < 1.5 || targets[i].parent != null)
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
        if (item != null)
        {
            Debug.Log(inventory.gameObject.name);
            inventory.AddItem(item);
        }
    }
}
