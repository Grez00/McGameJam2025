using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheepPrefab;
    [SerializeField] private int spawnRate = 2;
    [SerializeField] private float safeRadius = 0.5f;
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

    IEnumerator SpawnSheep()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Transform spawnPos = spawnPoints[Random.Range(1, spawnPoints.Length)];
            currentSheep = Instantiate(sheepPrefab, spawnPos.position, Quaternion.identity);

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

    public void SheepLost(GameObject lostSheep)
    {
        sheep.Remove(lostSheep);
    }

    public void ChangeSheepAnimation(AnimatorOverrideController newController)
    {

        foreach (GameObject sheepInstance in sheep)
        {

            Animator animator = sheepInstance.GetComponent<Animator>();
            Debug.Log(animator);
            if (animator != null)
            {
                animator.runtimeAnimatorController = newController;
            }
        }
    }
}
