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
    public GameObject sheepInstance;
    Animator animator;

    void Start()
    {
        sheep = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(SheepMove());
    }

    //randomly determines how sheep will move
    public IEnumerator SheepMove()
    {
        while (true)
        {
            if (loop)
            {
                if (Random.value <= runChance)
                {
                    RunAway();
                }
                else if (Random.value < .85) {
                    Wander();
                }
                else{
                    Wait();
                }
            }
            yield return new WaitForSeconds(movementRate);
        }
    }

    //checks if sheep has been returned to pen.
    void Update()
    {
        if (Vector2.Distance(transform.position, sheepSpawner.transform.position) < sheepSpawner.GetRadius())
        {
            sheepSpawner.SheepLost(this.gameObject);
            this.gameObject.SetActive(false);
            manager.UpdateBalance();
        }
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