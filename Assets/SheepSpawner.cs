using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheepPrefab;
    //rate at which new sheep are created
    [SerializeField] private int spawnRate = 2;
    //radius in which sheep create money
    [SerializeField] private float safeRadius = 0.5f;
    //list of sheep in safe radius
    private List<GameObject> sheep = new List<GameObject>();
    private Transform[] spawnPoints;
    private GameObject currentSheep;
    public GameManager manager;
    
    void Start()
    {
        StartCoroutine(SpawnSheep());
        spawnPoints = GetComponentsInChildren<Transform>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            SheepMovement movement = currentSheep.GetComponent<SheepMovement>();
            movement.sheepSpawner = this;
            movement.manager = manager;

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
