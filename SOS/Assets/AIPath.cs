using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPath : MonoBehaviour
{
    public Transform[] path;
    NavMeshAgent _navMeshAgent;
    private int randomDestination;

    void Start()
    {
        randomDestination = (int)Random.Range(0f, path.Length);
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = path[randomDestination].position;

    }

    // Update is called once per frame
    void Update()
    {
        AIView obj = gameObject.GetComponentInChildren<AIView>();
        var dist = Vector3.Distance(path[randomDestination].position, _navMeshAgent.transform.position);
        if (dist < 1.5 || path[randomDestination].parent != null)
        {
            randomDestination = (int)Random.Range(0f, path.Length);
            _navMeshAgent.destination = path[randomDestination].position;
        }
    }
}
