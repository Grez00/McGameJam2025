using UnityEngine;

public class MoveAwayFromP : MonoBehaviour
{
    private Transform player; // No need to assign in the Inspector
    public float moveSpeed = 2f;
    public float triggerDistance = 5f;
    public KeyCode moveKey = KeyCode.Space;
    public string controllerButton = "A";

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
    }

    void Update()
    {
        if (player == null) return; // Avoid null reference errors

        if (Input.GetKey(moveKey) || Input.GetButton(controllerButton)) // Check if the move key or controller button is pressed
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < triggerDistance)
            {
                Vector2 directionAway = (transform.position - player.position).normalized;

                transform.position = (Vector2)transform.position + directionAway * moveSpeed * Time.deltaTime;
            }
        }
    }
}
