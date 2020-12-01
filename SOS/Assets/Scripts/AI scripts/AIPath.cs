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
    private Transform boatTarget;
    public Animator animator;

    void Start()
    {
        bool assigned = false;
        int charInt = 1;
        while(!assigned)
        {
            if(Selector_Script.CharacterInt == charInt || GameObject.Find(getCharacterFromNumber(charInt)) != null){
                charInt++;
            }
            else{
                this.gameObject.name = getCharacterFromNumber(charInt);
                assigned = true;
                GameObject character = this.transform.Find(this.gameObject.name).gameObject;
                this.animator = character.GetComponent<Animator>();
                character.SetActive(true);
                this.GetComponent<AIItemsInteraction>().animator = character.GetComponent<Animator>();
                this.GetComponent<AIPowerUps>().animator = character.GetComponent<Animator>();
            }
        }
        randomDestination = (int)Random.Range(0f, path.Length);
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.destination = path[randomDestination].position;
        followsPath = true;
        GameObject[] boatPosition = GameObject.FindGameObjectsWithTag("BoatTag");
        boatTarget = boatPosition[0].transform;
    }

    void Update()
    {
        if (animator.GetBool("Jumping"))
            animator.SetBool("Jumping", false);
        if (animator.GetBool("WasHit"))
        {
            //animator.SetBool("WasHit", false);
        }

        animator.SetBool("IsWalking", true);
        bool canWin = hasAllWinnableItems();
        int random = Random.Range(0,10);
        if(_navMeshAgent.velocity == Vector3.zero && (random == 6)){
            _navMeshAgent.Move(_navMeshAgent.transform.forward*-1.3f);
        }
        if(canWin)
        {
            _navMeshAgent.destination = boatTarget.position;
        }
        else
        {
            AIView obj = gameObject.GetComponentInChildren<AIView>();
            
            if(obj.ItemsPositions.Count > 0 || obj.PlayersPositions.Count > 0)
            {
                if(obj.ItemsPositions.Count > 0){
                    var itemToGrab = obj.ItemsPositions.First();
                    
                    itemTarget = GameObject.Find(itemToGrab.Item1);
                    if(itemTarget != null && itemTarget.activeSelf)
                    {
                        _navMeshAgent.destination = itemToGrab.Item2;
                        followsPath = false;
                    }
                    else
                    {
                        followsPath = true;
                        obj.ItemsPositions.Remove(obj.ItemsPositions.First());
                    }
                }
                else
                {
                    var playerToAttack = obj.PlayersPositions.First();
                    //Attack player here
                    followsPath = true;
                    _navMeshAgent.destination = path[randomDestination].position;
                }

            }
            else
            {
                followsPath = true;
                _navMeshAgent.destination = path[randomDestination].position;
            }
            
            var dist = Vector3.Distance(_navMeshAgent.destination, _navMeshAgent.transform.position);
            if(dist < 1.1)
            {
                if(followsPath)
                {
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

    private bool hasAllWinnableItems(){
        int itemCount = gameObject.transform.childCount;
        int winningItemsCount = 0;
        for (int i = 0; i < itemCount; i++)
        {
            IInventoryItem item = gameObject.transform.GetChild(i).GetComponent<IInventoryItem>();
            if (item != null && item.Name != null && (item.Name.Contains("Oar") || item.Name.Equals("FlareGun") || item.Name.Equals("EmergencyKit") || item.Name.Equals("Parachute")))
            {
                winningItemsCount ++;
            }
        }
        return (winningItemsCount == 5);
    }

    private string getCharacterFromNumber(int charInt)
    {
        switch (charInt)
        {
            case 1:
                return "Businesswoman";
            case 2:
                return "Pilot";
            case 3:
                return "Old Lady";
            case 4:
                return "Hippie";
            default:
                return "Pilot";
        }
    }
}
