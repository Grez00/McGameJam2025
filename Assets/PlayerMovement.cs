
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public static float speed = 5f;
    Animator animator;
    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private bool isMoving;
    private bool movingVertically;

    // for footsteps
    public AudioSource[] footstepSources;
    private AudioSource randomFootstep;
    private AudioSource prevFootstep = null;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 

        //change animation display according to direction
        if (movementDirection != Vector2.zero)
        {
            isMoving = true;
            animator.SetBool("IsMoving", isMoving);
            if (movementDirection.x != 0)
            {
                animator.SetFloat("DirectionX", movementDirection.x);
            }
            else if (movementDirection.x == 0)
            {
                movingVertically = true;
                animator.SetBool("IsMovingUpDown", movingVertically);
            }         

            // for footstep audio:
            if (prevFootstep != null) 
            {
                if (!prevFootstep.isPlaying) 
                {
                    prevFootstep.enabled = false;
                    randomFootstep = footstepSources[Random.Range(0, footstepSources.Length)];
                    randomFootstep.enabled = true;
                    prevFootstep = randomFootstep;
                }
            }
            else 
            {
                    randomFootstep = footstepSources[Random.Range(0, footstepSources.Length)];
                    randomFootstep.enabled = true;
                    prevFootstep = randomFootstep;               
            }
        }
        else
        {
            isMoving = false;
            animator.SetBool("IsMoving", isMoving);

            foreach (AudioSource footstep in footstepSources) 
            {
                footstep.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * speed;
    }

    public Vector2 getMovementDirection()
    {
        return movementDirection;
    }
}
