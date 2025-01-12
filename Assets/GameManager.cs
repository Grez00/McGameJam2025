using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int balance = 100; //total player money
    [SerializeField] private float difficulty = 1.0f; //determines bleedrate and sheep speed
    [SerializeField] private float difficultyIncreaseRate = 8; //rate at which difficulty increases
    [SerializeField] private float bleedRate = 5; //rate of money loss
    [SerializeField] private int sheepValue = 25; //money brought in by each sheep
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int max_diff = 10;
    public TextMeshProUGUI balanceText;
    private float startTime;
    private float gameDuration = 500f;
    
    public AnimatorOverrideController SchoolSheep; //why is this not showing up in the inspector?  
    public AnimatorOverrideController AngelSheep;
    public AnimatorOverrideController NinjaSheep;
    public AnimatorOverrideController MusicSheep;
    public AnimatorOverrideController SherlockSheep;

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
                //balance -= 50;
                //GameObject lootBox = Instantiate(boxPrefab, new Vector3(mainCamera.position.x, mainCamera.position.y, -2), Quaternion.identity);
                //lootBox.GetComponentInChildren<Crate>().manager = this;
            }
        }
        balanceText.text = "$" + balance;

        Debug.Log("Balance: " + balance);
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
            int randNum = Random.Range(0, 5);
            gacha(randNum);
        }
    }

    //gradually increases difficulty
    IEnumerator Bleed()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyIncreaseRate);
            if (difficulty < max_diff) difficulty += 0.5f;
        }
    }

    public float getDifficulty()
    {
        return difficulty;
    }

    public int UpdateBalance(){
        balance += sheepValue;
        return balance;
    }

    public int getBalance(){
        return balance;
    }

    public void gameOver()
    {
        SceneManager.LoadScene (sceneName:"MetaEndScreen");
    }

    public void gameEnd()
    {

    }

    public void gacha(int rand) {
        List<string> upgrades = new List<string> {"SchoolSheep", "AngelSheep", "NinjaSheep", "MusicSheep", "SherlockSheep"};
        switch (upgrades[rand]) {
            /*
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
            */
            case "SchoolSheep":
                // TODO : VISUALS
                sheepSpawner.ChangeSheepAnimation(SchoolSheep);
                //upgrades.Remove("SchoolSheep");
                break;

            case "AngelSheep":
                // TODO : VISUALS
                sheepSpawner.ChangeSheepAnimation(AngelSheep);
                //upgrades.Remove("AngelSheep");
                break;

            case "NinjaSheep":
                // TODO : VISUALS
                sheepSpawner.ChangeSheepAnimation(NinjaSheep);
                //upgrades.Remove("NinjaSheep");
                break;

            case "MusicSheep":
                // TODO : VISUALS
                sheepSpawner.ChangeSheepAnimation(MusicSheep);
                //upgrades.Remove("MusicSheep");
                break;

            case "SherlockSheep":
                // TODO : VISUALS
                sheepSpawner.ChangeSheepAnimation(SherlockSheep);
                //upgrades.Remove("SherlockSheep");
                break;

            default:
                break;
        }
    }
}