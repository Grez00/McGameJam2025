using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    // Animator animator;

    // private bool isMoving;

    void Awake()
    {
        // animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movementInput = new Vector2 (moveX, moveY);

        //change animation display according to direction
        if (movementInput != Vector2.zero)
        {
            // isMoving = true;
            // animator.SetBool("IsMoving", isMoving);
            // animator.SetFloat("DirectionX", movementInput.x);
        }
        else
        {
            // isMoving = false;
            // animator.SetBool("IsMoving", isMoving);
        }
        transform.Translate(movementInput.normalized * speed * Time.deltaTime);   
    }
}
