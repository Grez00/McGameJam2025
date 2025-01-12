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

    IEnumerator SpawnSheep()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Transform spawnPos = spawnPoints[Random.Range(1, spawnPoints.Length)];

            currentSheep = Instantiate(sheepPrefab, spawnPos.position, Quaternion.identity);
            if (Random.value <= .5)
            {
                int randNum = Random.Range(0, 6);
                manager.gachaSingle(currentSheep, randNum);
            }

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
            if (Random.value <= .25)
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

    public void ChangeSingleSheep(GameObject sheepInstance, AnimatorOverrideController newController)
    {
        Animator animator = sheepInstance.GetComponent<Animator>();
        Debug.Log(animator);
        if (animator != null)
        {
            animator.runtimeAnimatorController = newController;
        }
    }
}
