using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public List<Vector3> waterRandomPositions;
    public List<Vector3> landRandomPositions;
    public List<Vector3> bananaPositions;
    public List<Vector3> shieldPositions;
    public List<Vector3> coconutPositions;

    public GameObject boat;
    public GameObject oars;
    public GameObject oar2;
    public GameObject banana;
    public GameObject flareGun;
    public GameObject shield;
    public GameObject coconut;

    void Start()
    {
        ValidInstantiationLandPositionsGenerator();
        ValidInstantiationWaterPositionsGenerator();
        RandomSpawner(boat, waterRandomPositions, true);
        RandomSpawner(oars, landRandomPositions, false);
        RandomSpawner(oar2, landRandomPositions, false);
        RandomSpawner(flareGun,landRandomPositions,false);

        /*bananaPositions.Add(new Vector3(20, 5, 34));
        RandomSpawner(banana, bananaPositions, false);

        shieldPositions.Add(new Vector3(4, 3, 25));
        RandomSpawner(shield, shieldPositions, false);*/

        SpawnAllCoconuts();
        SpawnAllBananas();
        SpawnAllShields();
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
        int randomIndex = UnityEngine.Random.Range(0, randomPositions.Count);
        GameObject clone = Instantiate(objectToInstantiate, randomPositions[randomIndex], Quaternion.identity) as GameObject;
        clone.SetActive(true);
        if (isBoat)
        {
            clone.tag = "BoatTag";
        }
        else
        {
            clone.tag = "Item";
            try {
            clone.transform.GetChild(0).gameObject.SetActive(true);
            }
            catch (Exception) {
                Debug.Log("AAAAAA");
            }
        }
        //Prevents objects overlapping
        randomPositions.RemoveAt(randomIndex);
    }

    private void SpawnAllCoconuts()
    {
        SpawnAllItems("CoconutSpawn", coconut);
    }

    private void SpawnAllBananas()
    {
        SpawnAllItems("BananaSpawn", banana);
    }

    public void SpawnAllShields()
    {
        SpawnAllItems("ShieldSpawn", shield);
    }

    private void SpawnAllItems(string spawnTag, GameObject objectToSpawn)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnTag);
        foreach(GameObject spawnPoint in spawnPoints)
        {
            Transform spawnTransform = spawnPoint.transform;
            /*GameObject clone = */Instantiate(objectToSpawn, new Vector3(spawnTransform.position.x, spawnTransform.position.y, spawnTransform.position.z), Quaternion.identity) /*as GameObject*/;            
            //clone.SetActive(true);
        }
    }

    public void SpawnCoconut()
    {
        SpawnObject("CoconutSpawn", coconut);
    }

    public void SpawnBanana()
    {
        SpawnObject("BananaSpawn", banana);
    }

    public void SpawnShield()
    {
        SpawnObject("ShieldSpawn", shield);
    }

    private void SpawnObject(string spawnTag, GameObject objectToSpawn)
    {
        GameObject[] possibleSpawnPoints = GameObject.FindGameObjectsWithTag(spawnTag);
        int spawnIndex = UnityEngine.Random.Range(0, possibleSpawnPoints.Length);
        GameObject chosenSpawn = possibleSpawnPoints[spawnIndex];
        Transform chosenSpawnTransform = chosenSpawn.transform;
        /*GameObject clone = */Instantiate(objectToSpawn, new Vector3(chosenSpawnTransform.position.x, chosenSpawnTransform.position.y, chosenSpawnTransform.position.z), Quaternion.identity) /*as GameObject*/;
        //clone.SetActive(true);
    }
}
