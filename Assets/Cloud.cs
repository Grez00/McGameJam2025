using UnityEngine;

public class Cloud : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float speed = 2f;
    public Vector3 startPosition = new Vector3(25f, 25f, 0f);

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
    }

    void Update()
    {
        transform.Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);

        if (transform.position.y < -45 || transform.position.x < -45)
        {
            transform.position = startPosition;
        }
    }
}
