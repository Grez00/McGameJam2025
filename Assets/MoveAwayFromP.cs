using System.Collections;
using UnityEngine;

public class MoveAwayFromP : MonoBehaviour
{
    private Transform player; // No need to assign in the Inspector
    public float moveSpeed = 2f;
    public float triggerDistance = 5f;
    public KeyCode moveKey = KeyCode.Space;
    public string controllerButton = "A";
    private SheepMovement movement;
    private float lastHit = -1f; // Stores the time of the last hit
    private float cooldown = 1f; // Minimum time between hits

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

        movement = GetComponent<SheepMovement>();
        if (movement == null) Debug.Log("NO MOVEMeNT");
    }

    void Update()
    {
        if (player == null) return; // Avoid null reference errors

        if (Input.GetKey(moveKey) || Input.GetButton(controllerButton)) // Check if the move key or controller button is pressed
        {
            if (Time.time-lastHit > cooldown) {

                lastHit = Time.time;

                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (distanceToPlayer < triggerDistance)
                {
                    StartCoroutine(HitSheep());
                    // transform.position = (Vector2)transform.position + directionAway * moveSpeed * Time.deltaTime;
                }
            }
        }
    }

    IEnumerator HitSheep() {
        Vector2 directionAway = (transform.position - player.position).normalized;
        movement.loop = false;
        //movement.setSheepVelocity((Vector2)transform.position + directionAway * moveSpeed * Time.deltaTime, movement.runSpeed);
        movement.setSheepVelocity(directionAway * moveSpeed, movement.runSpeed);
        yield return new WaitForSeconds(movement.movementRate * 3);
        movement.loop = true;
    }

}
