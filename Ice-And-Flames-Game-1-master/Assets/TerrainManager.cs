using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrainPrefabs = null;

    private List<GameObject> activeTiles = new List<GameObject>();
    private Transform playerTransform;
    private float spawnZ = -10.0f;

    public float prefabLength = 15.0f;
    public float safeZone = 10.0f;
    public int maxPrefabs = 5;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i= 0; i < maxPrefabs; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            } else
            {
                randomIndex = UnityEngine.Random.Range(1, terrainPrefabs.Length);
                SpawnTile(randomIndex);
            }
        }

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - maxPrefabs * prefabLength))
        {
            randomIndex = UnityEngine.Random.Range(1, terrainPrefabs.Length);
            SpawnTile(randomIndex);
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex)
    {
        GameObject go;
        go = Instantiate(terrainPrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += prefabLength;

        activeTiles.Add(go);
    }
}
