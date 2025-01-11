using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheepPrefab;
    //rate at which new sheep are created
    [SerializeField] private int spawnRate = 2;
    //radius in which sheep create money
    [SerializeField] private float safeRadius = 10;
    //list of sheep in safe radius
    private List<GameObject> sheep = new List<GameObject>();
    private Transform[] spawnPoints;
    private GameObject currentSheep;
    
    void Start()
    {
        StartCoroutine(SpawnSheep());
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        
    }

    //instantiates new sheep based on spawn rate
    IEnumerator SpawnSheep()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Transform spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];
            currentSheep = Instantiate(sheepPrefab, spawnPos.position, Quaternion.identity);
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

    //updates sheep list when one is lost
    public void SheepLost(GameObject lostSheep)
    {
        sheep.Remove(lostSheep);
    }
}
