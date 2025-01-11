using UnityEngine;
using System.Collections;


public class SheepMovement : MonoBehaviour
{
    private Rigidbody2D sheep;
    private SpriteRenderer sprite;
    [SerializeField] private int movementRate = 2; //determines how often a sheep changes its movement
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField, Range(0f, 1f)] private float runChance = .1f; //chance for sheep to run when changing movement
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private GameManager manager;
    private int currentDirection;
    Animator animator;
    
    void Start()
    {
        sheep = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(SheepMove());
        animator = GetComponent<Animator>();
    }

    //randomly determines how sheep will move
    IEnumerator SheepMove()
    {
        bool loop = true;
        while (loop)
        {
            yield return new WaitForSeconds(movementRate);
            if (Random.value <= runChance)
            {
                RunAway();
                loop = false;
            }
            else if (Random.value < .75) {
                Wander();
            }
            else{
                Wait();
            }
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

    //sets sheep velocity based on direction and speed. Flips sprite appropriately.
    private void setSheepVelocity(int direction, float speed)
    {
        sheep.linearVelocity = new Vector2(speed * Mathf.Sin(Mathf.Rad2Deg * direction), speed * Mathf.Cos(Mathf.Rad2Deg * direction));
        sprite.flipX = sheep.linearVelocity.x > 0f;
    }

    //chooses random direction and moves according to runSpeed
    private void RunAway()
    {
        currentDirection = getNewDirection();
        setSheepVelocity(currentDirection, runSpeed);
        
        //set sheep animation parameters accordingly to make the sheep animation move in proper speed and diretion
        animator.SetBool("IsRunning", true);
        if (currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360)
        {
            animator.SetFloat("DirectionX", 1f);
        }
        else
        {
            animator.SetFloat("DirectionX", -1f);
        }
        
    }

    //chooses random direction and moves according to walkSpeed
    private void Wander()
    {
        currentDirection = getNewDirection();
        setSheepVelocity(currentDirection, walkSpeed);

        //set sheep animation parameters accordingly
        animator.SetBool("IsWalking", true);
        if (currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360)
        {
            animator.SetFloat("DirectionX", 1f);
        }
        else
        {
            animator.SetFloat("DirectionX", -1f);
        }
    }

    //stops all movement
    private void Wait()
    {
        sheep.linearVelocity = Vector2.zero;

        //set sheep animation parameters accordingly
        animator.SetBool("IsIdle", true);
        if (currentDirection > 0 && currentDirection < 90 || currentDirection > 270 && currentDirection < 360)
        {
            animator.SetFloat("DirectionX", 1f);
        }
        else
        {
            animator.SetFloat("DirectionX", -1f);
        }
    }
}