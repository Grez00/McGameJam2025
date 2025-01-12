using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
using System.Collections.Generic;

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
    private float startTime;
    private float gameDuration = 500f;


    void Start()
    {
        startTime = Time.time;
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
        //balanceText.text = "$" + balance;

        if (balance < 0)
        {
            gameOver();
        }
        else if ((Time.time - startTime) >= gameDuration)
        {
            gameEnd();
        }
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

    public void gameOver()
    {

    }

    public void gameEnd()
    {

    }

    public void gacha(float rand) {
        List<string> upgrades = new List<string> {"SpeedUpgrade", "CaneRange", "SchoolSheep", "AngelSheep", "NinjaSheep", "MusicSheep", "SherlockSheep"};
        switch (upgrades[(int)(rand * upgrades.Count)]) {
            case "SpeedUpgrade":
                // TODO : VISUALS
                PlayerMovement.speed *= 1.4f;
                break;

            case "CaneRange":
                // TODO : VISUALS
                MoveAwayFromP.triggerDistance *= 1.3f;
                break;

            case "CaneCooldown":
                // TODO : VISUALS
                MoveAwayFromP.cooldown *= 0.7f;
                break;

            case "SchoolSheep":
                // TODO : VISUALS
                // TODO : SET SKIN
                upgrades.Remove("SchoolSheep");
                break;

            case "AngelSheep":
                // TODO : VISUALS
                // TODO : SET SKIN
                upgrades.Remove("AngelSheep");
                break;

            case "NinjaSheep":
                // TODO : VISUALS
                // TODO : SET SKIN
                upgrades.Remove("NinjaSheep");
                break;

            case "MusicSheep":
                // TODO : VISUALS
                // TODO : SET SKIN
                upgrades.Remove("MusicSheep");
                break;

            case "SherlockSheep":
                // TODO : VISUALS
                // TODO : SET SKIN
                upgrades.Remove("SherlockSheep");
                break;

            default:
                break;
        }
    }
}
