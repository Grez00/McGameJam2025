

using System.Collections;
using UnityEngine;

public class MoveAwayFromP : MonoBehaviour
{
    private Transform player;
    private Transform spawnPoint;
    private GameManager gameManager;
    public float moveSpeed = 2f;
    public float triggerDistance = 5f;
    public KeyCode moveKey = KeyCode.Space;
    public string controllerButton = "Jump";
    private SheepMovement movement;
    private float lastHit = -1f; // Stores the time of the last hit
    private float cooldown = 1f; // Minimum time between hits
    // Multiplier for the wait time after hitting the sheep
    public float waitTimeMultiplier = 3f;

    IEnumerator HitSheep()
    {
        Vector2 directionAway = (transform.position - player.position).normalized;
        movement.loop = false;
        movement.setSheepVelocity(directionAway, movement.runSpeed);
        yield return new WaitForSeconds(movement.movementRate * waitTimeMultiplier);
        movement.loop = true;
    }


    void Start()
    {
        // Find the player by tag or name
        GameObject playerObject = GameObject.FindWithTag("Player"); // Make sure your player has the tag "Player"
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's Transform
        }
        else
        {
            Debug.LogError("Player not found! Ensure your player object is tagged as 'Player'.");
        }

        GameObject respawnObject = GameObject.FindWithTag("Respawn"); // Make sure your respawn has the tag "Respawn"
            if (respawnObject != null)
            {
                spawnPoint = respawnObject.transform; // Assign the spawn point's Transform
            }
            else
            {
                Debug.LogError("Respawn not found! Ensure your respawn object is tagged as 'Respawn'.");
            }

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager"); // Make sure your game manager has the tag "GameController"
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>(); // Assign the GameManager component
        }
        else
        {
            Debug.LogError("GameManager not found! Ensure your gameManager object is tagged as 'GameController'.");
        }

        movement = GetComponent<SheepMovement>();
        if (movement == null) Debug.LogError("No movement component found!");

    }

    void Update()
    {
        if (player == null) return; // Avoid null reference errors

        Vector2 directionAway = Vector2.zero;
        if (Input.GetKey(moveKey) || Input.GetButtonDown("Jump")) // Check if the move key or controller button is pressed
        {
            if (Time.time-lastHit > cooldown) {

                lastHit = Time.time;

                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                directionAway = (transform.position - player.position).normalized;

                if (distanceToPlayer < triggerDistance)
                {
                    StartCoroutine(HitSheep());
                    // transform.position = (Vector2)transform.position + directionAway * moveSpeed * Time.deltaTime;
                }

                SheepSpawner sheepSpawner = spawnPoint.GetComponent<SheepSpawner>();
                if (sheepSpawner != null && Vector2.Distance(transform.position, spawnPoint.position) < sheepSpawner.GetRadius())
                {
                    if (gameManager != null) gameManager.UpdateMoney();
                    else Debug.LogError("GameManager is not initialized.");
         
                    Debug.Log("Sheep returned to pen");
                    movement.setSheepVelocity(directionAway * moveSpeed, movement.runSpeed);
                }
            }
            else
            {
                Debug.LogError("GameManager is not initialized.");
            }
            Debug.Log("Sheep returned to pen");

            if (movement == null) Debug.LogError("No movement component found at move time!");
            movement.setSheepVelocity(directionAway * moveSpeed, movement.runSpeed);
        }
    }
}




