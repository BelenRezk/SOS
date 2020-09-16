using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public List<Vector3> randomPositions;
    
    //TODO: Create GameObject variables for each object type (coconut, boat oar, etc.) 
    public GameObject objectToInstantiate;
     
    void Start () 
    {
        ValidInstantiationPositionsGenerator();
        RandomSpawner();
    }

    void ValidInstantiationPositionsGenerator()
    {
        randomPositions.Add(new Vector3(2,2,6));
        randomPositions.Add(new Vector3(-2,2,6));
    }

    void RandomSpawner()
    {
        int randomIndex = Random.Range(0, randomPositions.Count);
        Instantiate(objectToInstantiate, randomPositions[randomIndex], Quaternion.identity);
        //Prevents objects overlapping
        randomPositions.RemoveAt(randomIndex);
    }
}
