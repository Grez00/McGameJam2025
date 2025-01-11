using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheepPrefab;
    [SerializeField] private int spawnRate = 2;
    [SerializeField] private float safeRadius = 10;
    private List<GameObject> sheep = new List<GameObject>();
    private GameObject currentSheep;
    
    void Start()
    {
        StartCoroutine(SpawnSheep());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnSheep()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            currentSheep = Instantiate(sheepPrefab, transform.position, Quaternion.identity);
            sheep.Add(currentSheep);
        }
    }

    public int GetSheepCount()
    {
        return sheep.Count;
    }

    public float GetRadius()
    {
        return safeRadius;
    }

    public void SheepLost(GameObject lostSheep)
    {
        sheep.Remove(lostSheep);
    }
}
