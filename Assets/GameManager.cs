using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int Balance = 100; //total player money
    [SerializeField] private float difficulty = 1.0f; //determines bleedrate and sheep speed
    [SerializeField] private float difficultyIncreaseRate = 11; //rate at which difficulty increases
    [SerializeField] private float bleedRate = 5; //rate of money loss
    [SerializeField] private int sheepValue = 1; //money brought in by each sheep
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private int max_diff = 10; 
    public TextMeshProUGUI balanceText;


    void Start()
    {
        StartCoroutine(UpdateMoney());
        StartCoroutine(Bleed());
    }

    void Update()
    {
        bleedRate = difficulty + 4;
        balanceText.text = "$" + Balance;
    }

    //updates balance based on income
    public IEnumerator UpdateMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int income = (int)(-bleedRate + (sheepSpawner.GetSheepCount() * sheepValue));
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
            yield return new WaitForSeconds(difficultyIncreaseRate);
            if (difficulty < max_diff) difficulty += 0.1f;
        }
    }

    public float getDifficulty()
    {
        return difficulty;
    }

    public int updateBalance(){
        Balance += sheepValue;
        return Balance;
    }
    public int getBalance(){
        return Balance;
    }
}
