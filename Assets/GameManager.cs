using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //total player money
    private int Balance = 100;
    //determines bleedrate and sheep speed
    [SerializeField] private int difficulty = 1;
    //rate of money loss
    [SerializeField] private int bleedRate = 5;
    //money brought in by each sheep
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

    //updates balance based on income
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

    //gradually increases difficulty
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
