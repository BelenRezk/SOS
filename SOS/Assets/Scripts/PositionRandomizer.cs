using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public List<Vector3> waterRandomPositions;
    public List<Vector3> landRandomPositions;
     
    public GameObject boat;
    public GameObject oars;
     
    void Start () 
    {
        ValidInstantiationLandPositionsGenerator();
        ValidInstantiationWaterPositionsGenerator();
        RandomSpawner(boat, waterRandomPositions);
        RandomSpawner(oars,landRandomPositions);
    }

    void ValidInstantiationLandPositionsGenerator()
    {
        landRandomPositions.Add(new Vector3(2,2,6));
        landRandomPositions.Add(new Vector3(-2,2,6));
    }

    void ValidInstantiationWaterPositionsGenerator()
    {
        waterRandomPositions.Add(new Vector3(1,4,3));
        waterRandomPositions.Add(new Vector3(-1,4,3));
    }

    void RandomSpawner(GameObject objectToInstantiate, List<Vector3> randomPositions)
    {
        int randomIndex = Random.Range(0, randomPositions.Count);
        GameObject clone = Instantiate(objectToInstantiate, randomPositions[randomIndex], Quaternion.identity) as GameObject;
        clone.SetActive(true);
        //Prevents objects overlapping
        randomPositions.RemoveAt(randomIndex);
    }
}
