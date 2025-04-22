using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] titlePrefabs;
    public float zSpawn = 0;
    public float titleLength;

    void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(4);
    }

    void Update()
    {
        
    }

    public void SpawnTile(int tileIndex)
    {
        Instantiate(titlePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += titleLength;
    }
}