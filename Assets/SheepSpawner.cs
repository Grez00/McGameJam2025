using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheepPrefab;
    //rate at which new sheep are created
    [SerializeField] private float spawnRate = 1f;
    //radius in which sheep create money
    [SerializeField] private float safeRadius = 2f;
    //list of existing sheep safe radius
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
            Transform spawnPos = spawnPoints[Random.Range(1, spawnPoints.Length)];
            currentSheep = Instantiate(sheepPrefab, spawnPos.position, Quaternion.identity);

            //make sheep sprite smaller
            currentSheep.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            SheepMovement movement = currentSheep.GetComponent<SheepMovement>();
            movement.sheepSpawner = this;
            movement.manager = manager;
            movement.sheepInstance = currentSheep;

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

    //updates sheep list when one is returned
    public void SheepLost(GameObject lostSheep)
    {
        sheep.Remove(lostSheep);
    }
}
