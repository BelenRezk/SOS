using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIPath : MonoBehaviour
{
    public Transform[] path;
    NavMeshAgent _navMeshAgent;
    private int randomDestination;
    private bool followsPath;
    private GameObject itemTarget;

    void Start()
    {
        randomDestination = (int)Random.Range(0f, path.Length);
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = path[randomDestination].position;
        followsPath = true;

    }

    void Update()
    {
        AIView obj = gameObject.GetComponentInChildren<AIView>();
        
        if(obj.ItemsPositions.Count > 0 || obj.PlayersPositions.Count > 0){
            if(obj.ItemsPositions.Count > 0){
                var itemToGrab = obj.ItemsPositions.First();
                
                itemTarget = GameObject.Find(itemToGrab.Item1);
                if(itemTarget.activeSelf){
                    _navMeshAgent.destination = itemToGrab.Item2;
                    followsPath = false;
                }
            }
            else{
                var playerToAttack = obj.PlayersPositions.First();
                //Attack player here
                followsPath = true;
                _navMeshAgent.destination = path[randomDestination].position;
            }

        }
        else{
            followsPath = true;
            _navMeshAgent.destination = path[randomDestination].position;
        }
        
        var dist = Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position);
        if(dist < 1.54)
        {
            if(followsPath){
                randomDestination = (int)Random.Range(0f, path.Length);
            }
            else if(itemTarget!=null)
            {
                obj.ItemsPositions.Remove(obj.ItemsPositions.First());
                followsPath = true;
                itemTarget = null;
            }
            
            _navMeshAgent.destination = path[randomDestination].position;
        }
    }
}
