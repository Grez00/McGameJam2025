using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int Balance = 100;
    [SerializeField] private int difficulty = 1;
    [SerializeField] private int bleedRate = 5;
    [SerializeField] private int sheepValue = 1;
    [SerializeField] private SheepSpawner sheepSpawner;

    void Start()
    {
        StartCoroutine(UpdateMoney());
        StartCoroutine(Bleed());
    }

    void Update()
    {
        bleedRate = difficulty + 4;
    }

    IEnumerator UpdateMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int income = -bleedRate + (sheepSpawner.GetSheepCount() * sheepValue);
            Balance = Balance + income;
            Debug.Log("Cash: " + Balance);
            Debug.Log("Income: " + income);
        }
    }

    IEnumerator Bleed()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            difficulty += 1;
        }
    }

    public int getDifficulty()
    {
        return difficulty;
    }
}
