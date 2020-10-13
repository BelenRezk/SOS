using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public List<Vector3> waterRandomPositions;
    public List<Vector3> landRandomPositions;
    public List<Vector3> bananaPositions;

    public GameObject boat;
    public GameObject oars;
    public GameObject banana;
    public GameObject flareGun;

    void Start()
    {
        ValidInstantiationLandPositionsGenerator();
        ValidInstantiationWaterPositionsGenerator();
        RandomSpawner(boat, waterRandomPositions, true);
        RandomSpawner(oars, landRandomPositions, false);
        RandomSpawner(flareGun,landRandomPositions,false);

        //bananaPositions.Add(new Vector3(4, 2, 8));
        bananaPositions.Add(new Vector3(4, 2, 6));
        RandomSpawner(banana, bananaPositions, false);
    }

    void ValidInstantiationLandPositionsGenerator()
    {
        landRandomPositions.Add(new Vector3(1, 2, 6));
        landRandomPositions.Add(new Vector3(-4 ,2, 6));
    }

    void ValidInstantiationWaterPositionsGenerator()
    {
        waterRandomPositions.Add(new Vector3(10, 1, 3));
        waterRandomPositions.Add(new Vector3(15, 1, 3));
    }

    void RandomSpawner(GameObject objectToInstantiate, List<Vector3> randomPositions, bool isBoat)
    {
        int randomIndex = Random.Range(0, randomPositions.Count);
        GameObject clone = Instantiate(objectToInstantiate, randomPositions[randomIndex], Quaternion.identity) as GameObject;
        clone.SetActive(true);
        if (isBoat)
        {
            clone.tag = "BoatTag";
        }
        else
        {
            clone.tag = "CollectiblesTag";
        }
        //Prevents objects overlapping
        randomPositions.RemoveAt(randomIndex);
    }
}
