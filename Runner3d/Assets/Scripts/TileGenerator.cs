using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 100;
    [SerializeField] private Transform player;
    private int startTiles = 5;

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(i);
            //if (i == 0)
            //    SpawnTile(0);
            //else
            //    SpawnTile(Random.Range(1, tilePrefabs.Length));
        }        
    }

    void Update()
    {
        //if (player.position.z - 120 > spawnPos - (startTiles * tileLength))
        //{
        //    SpawnTile(Random.Range(1, tilePrefabs.Length));
        //    DestroyTile();
        //}
    }        
    

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }

    private void DestroyTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
