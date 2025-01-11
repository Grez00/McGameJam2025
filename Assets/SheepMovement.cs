using UnityEngine;
using System.Collections;


public class SheepMovement : MonoBehaviour
{
    private Rigidbody2D sheep;
    private SpriteRenderer sprite;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField, Range(0f, 1f)] private float runChance = .1f;
    [SerializeField] private SheepSpawner sheepSpawner;
    [SerializeField] private GameManager manager;
    private int currentDirection;
    
    void Start()
    {
        sheep = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(SheepMove());
    }

    IEnumerator SheepMove()
    {
        bool loop = true;
        while (loop)
        {
            yield return new WaitForSeconds(2);
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

    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) > sheepSpawner.GetRadius())
        {
            sheepSpawner.SheepLost(sheep.gameObject);
        }
        runSpeed = manager.getDifficulty() + 1;
        runChance = manager.getDifficulty() * .1f; 
    }   

    private int getNewDirection()
    {
        return Random.Range(0, 359);
    }

    private void setSheepVelocity(int direction, float speed)
    {
        sheep.linearVelocity = new Vector2(speed * Mathf.Sin(Mathf.Rad2Deg * direction), speed * Mathf.Cos(Mathf.Rad2Deg * direction));
        sprite.flipX = sheep.linearVelocity.x > 0f;
    }

    private void RunAway()
    {
        currentDirection = getNewDirection();
        setSheepVelocity(currentDirection, runSpeed);
    }

    private void Wander()
    {
        currentDirection = getNewDirection();
        setSheepVelocity(currentDirection, walkSpeed);
    }

    private void Wait()
    {
        sheep.linearVelocity = Vector2.zero;
    }
}