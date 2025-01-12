using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
