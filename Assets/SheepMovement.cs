using UnityEngine;
using System.Collections;

public class SheepMovement : MonoBehaviour
{
    public bool loop = true;
    private Rigidbody2D sheep;
    private SpriteRenderer sprite;
    public int movementRate = 2; //determines how often a sheep changes its movement
    [SerializeField] private float walkSpeed = 1f;
    public float runSpeed = 1f;
    [SerializeField, Range(0f, 1f)] private float runChance = .1f; //chance for sheep to run when changing movement
    public SheepSpawner sheepSpawner;
    public GameManager manager;
    private int currentDirection;
    Animator animator;

    void Start()
    {
        sheep = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(SheepMove());
        
    }

    //randomly determines how sheep will move
    IEnumerator SheepMove()
    {
        while (loop)
        {
            if (Random.value <= runChance)
            {
                RunAway();
                loop = false;
            }
            else if (Random.value < .85) {
                Wander();
            }
            else{
                Wait();
            }
            yield return new WaitForSeconds(movementRate);
        }
    }

    //checks if sheep is lost. increments runSpeed and runChance based on difficulty
    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) > sheepSpawner.GetRadius())
        {
            sheepSpawner.SheepLost(sheep.gameObject);
        }

        runSpeed = manager.getDifficulty() + 1;
        runChance = manager.getDifficulty() * .1f;

        //if the speed exceeds a threshold, the sheep feet moves faster
    }

    private int getNewDirection()
    {
        return Random.Range(0, 359);
    }

    private void setSheepForce(int direction, float speed)
    {
        Vector2 force = new Vector2(speed * Mathf.Sin(Mathf.Deg2Rad * direction), speed * Mathf.Cos(Mathf.Deg2Rad * direction));
        sheep.AddForce(force);
        sprite.flipX = force.x > 0f;
    }

    private void RunAway()
    {
        currentDirection = getNewDirection();
        setSheepForce(currentDirection, runSpeed);

        animator.SetBool("IsRunning", true);
        animator.SetFloat("DirectionX", currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360 ? 1f : -1f);
    }

    private void Wander()
    {
        currentDirection = getNewDirection();
        setSheepForce(currentDirection, walkSpeed);

        animator.SetBool("IsWalking", true);
        animator.SetFloat("DirectionX", currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360 ? 1f : -1f);
    }

    private void Wait()
    {
        sheep.linearVelocity = Vector2.zero;
        sheep.linearVelocity = Vector2.zero;

        animator.SetBool("IsIdle", true);
        animator.SetFloat("DirectionX", currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360 ? 1f : -1f);
    }
}