using System.Collections;
using UnityEngine;

public class MoveAwayFromP : MonoBehaviour
{
    private Transform player;
    private PlayerMovement playerMovement;
    private Transform spawnPoint;
    private GameManager gameManager;
    public float moveSpeed = 1f;
    public static float triggerDistance = 1.5f;
    public KeyCode moveKey = KeyCode.Space;
    public string controllerButton = "Jump";
    private SheepMovement movement;
    private float lastHit = -1f; // Stores the time of the last hit
    public static float cooldown = 1f; // Minimum time    between hits
    // Multiplier for the wait time after hitting the sheep
    public float waitTimeMultiplier = 1.25f;
    private Rigidbody2D rb;

    IEnumerator HitSheep()
    {
        Vector2 directionAway = (transform.position - player.position).normalized;

        Debug.Log("Sheep hit!");
        movement.loop = false;

        rb.linearVelocity = playerMovement.getMovementDirection() * playerMovement.speed;
        yield return new WaitForSeconds(waitTimeMultiplier);

        rb.linearVelocity = Vector2.zero;
        movement.loop = true;

        Debug.Log("Sheep moving again");
    }

    void Start()
    {
        // Find the player by tag or name
        GameObject playerObject = GameObject.FindWithTag("Player"); // Make sure your player has the tag "Player"
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's Transform
            playerMovement = playerObject.GetComponent<PlayerMovement>();
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

        GameObject gameManagerObject = GameObject.FindWithTag("GameController"); // Make sure your game manager has the tag "GameController"
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>(); // Assign the GameManager component
        }
        else
        {
            Debug.LogError("GameManager not found! Ensure your gameManager object is tagged as 'GameController'.");
        }

        movement = GetComponent<SheepMovement>();
        rb = GetComponent<Rigidbody2D>();
        if (movement == null) Debug.LogError("No movement component found!");
        if (rb == null) Debug.LogError("No Rigidbody2D component found!");
    }

    void Update()
    {
        if (player == null) return; // Avoid null reference errors

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < triggerDistance)
        {
            if (Input.GetKey(moveKey) || Input.GetButtonDown("Jump")) // Check if the move key or controller button is pressed
            {
                if (Time.time - lastHit > cooldown)
                {
                    lastHit = Time.time;

                    Vector2 directionAway = (transform.position - player.position).normalized;

                    StartCoroutine(HitSheep());
                    /*
                    if (spawnPoint != null)
                    {
                        SheepSpawner sheepSpawner = spawnPoint.GetComponent<SheepSpawner>();
                        if (sheepSpawner != null && Vector2.Distance(transform.position, spawnPoint.position) < sheepSpawner.GetRadius())
                        {
                            if (gameManager != null) gameManager.UpdateMoney();
                            else Debug.LogError("GameManager is not initialized.");

                            Debug.Log("Sheep returned to pen");
                            rb.linearVelocity = directionAway * moveSpeed;
                        }
                    }
                    */
                }
            }
        }
    }
}