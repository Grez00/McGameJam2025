using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private int balance = 100; //total player money
    [SerializeField] private float difficulty = 1.0f; //determines bleedrate and sheep speed
    [SerializeField] private float difficultyIncreaseRate = 10; //rate at which difficulty increases
    [SerializeField] private float bleedRate = 5; //rate of money loss
    [SerializeField] private int sheepValue = 1; //money brought in by each sheep
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject boxPrefab;

    void Start()
    {
        StartCoroutine(UpdateMoney());
        StartCoroutine(Bleed());
    }

    void Update()
    {
        bleedRate = difficulty + 4;

        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F Pressed");
            if (balance > 50)
            {
                balance -= 50;
                GameObject lootBox = Instantiate(boxPrefab, Vector3.zero, Quaternion.identity);
                lootBox.GetComponentInChildren<Crate>().manager = this;
            }
        }
    }

    //updates balance based on income
    public IEnumerator UpdateMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int income = (int)(-bleedRate + (sheepSpawner.GetSheepCount() * sheepValue));
            balance = balance + income;
            Debug.Log("Cash: " + balance);
            Debug.Log("Income: " + income);
        }
    }

    //gradually increases difficulty
    IEnumerator Bleed()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyIncreaseRate);
            difficulty += 1;
        }
    }

    public float getDifficulty()
    {
        return difficulty;
    }

    public void prizeReceived(int prizeNum)
    {
        Debug.Log("Upgrade Recieved");
        if (prizeNum == 0)
        {
            Debug.Log("Speed Increased");
            player.speed += 2;
        }
        else if (prizeNum == 1)
        {
            Debug.Log("Difficulty Decreased");
            difficulty -= 2;
        }
        else
        {
            Debug.Log("Rent Decreased");
            bleedRate -= 2;
        }
    }
}
