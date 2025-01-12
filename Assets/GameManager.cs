using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int balance = 100; //total player money
    [SerializeField] private float difficulty = 1.0f; //determines bleedrate and sheep speed
    [SerializeField] private float difficultyIncreaseRate = 10; //rate at which difficulty increases
    [SerializeField] private float bleedRate = 5; //rate of money loss
    [SerializeField] private int sheepValue = 25; //money brought in by each sheep
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int max_diff = 10;
    public TextMeshProUGUI balanceText;


    void Start()
    {
        StartCoroutine(RentDrain());
        StartCoroutine(Bleed());
    }

    void Update()
    {
        bleedRate = difficulty + 4;

        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F Pressed");
            if (balance >= 50)
            {
                balance -= 50;
                GameObject lootBox = Instantiate(boxPrefab, Vector3.zero, Quaternion.identity);
                //lootBox.GetComponentInChildren<Crate>().manager = this;
            }
        }
        balanceText.text = "$" + balance;
    }

    //updates balance based on income
    public IEnumerator RentDrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            balance -= (int) bleedRate;
            Debug.Log("Cash: " + balance);
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

    public int UpdateBalance(){
        balance += sheepValue;
        Debug.Log("Cash: " + balance);
        return balance;
    }

    public int getBalance(){
        return balance;
    }

    public void prizeReceived()
    {

    }

    public void gacha(float rand) {
        string[] upgrades = {"SpeedUpgrade", "SchoolSheep", "AngelSheep", "NinjaSheep", "MusicSheep", "SherlockSheep" };
        switch (upgrades[(int)(rand * upgrades.Length)]) {
            case "SpeedUpgrade":

                break;
            case "SchoolSheep":

                break;
            case "AngelSheep":

                break;
            case "NinjaSheep":

                break;
            case "MusicSheep":

                break;
            case "SherlockSheep":

                break;
            default:
                break;
        }
    }
}
