using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public List<Vector3> waterRandomPositions;
    public List<Vector3> landRandomPositions;
    public List<Vector3> bananaPositions;
    public List<Vector3> shieldPositions;

    public GameObject boat;
    public GameObject oars;
    public GameObject oar2;
    public GameObject banana;
    public GameObject flareGun;
    public GameObject shield;

    void Start()
    {
        ValidInstantiationLandPositionsGenerator();
        ValidInstantiationWaterPositionsGenerator();
        RandomSpawner(boat, waterRandomPositions, true);
        RandomSpawner(oars, landRandomPositions, false);
        RandomSpawner(oar2, landRandomPositions, false);
        RandomSpawner(flareGun,landRandomPositions,false);

        bananaPositions.Add(new Vector3(20, 5, 34));
        RandomSpawner(banana, bananaPositions, false);

        shieldPositions.Add(new Vector3(4, 3, 25));
        RandomSpawner(shield, shieldPositions, false);
    }

    void ValidInstantiationLandPositionsGenerator()
    {
        landRandomPositions.Add(new Vector3(8, 3, 29));
        landRandomPositions.Add(new Vector3(18, 4, -80));
        landRandomPositions.Add(new Vector3(190, 15, -50));
    }

    void ValidInstantiationWaterPositionsGenerator()
    {
        waterRandomPositions.Add(new Vector3(-14, 1, -28));
        waterRandomPositions.Add(new Vector3(-6, 1, 15));
        waterRandomPositions.Add(new Vector3(0, 1, 39));
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
            clone.tag = "Item";
        }
        //Prevents objects overlapping
        randomPositions.RemoveAt(randomIndex);
    }
}
