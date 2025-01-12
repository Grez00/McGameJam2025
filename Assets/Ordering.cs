using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DepthSorter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float offset;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObject.name.Contains("Player"))
        {
            offset = 0.3f;
        }
        else if (gameObject.name.Contains("Barn"))
        {
            offset = 0.8f;
        }
        else if (gameObject.name.Contains("Sheep"))
        {
            offset = 0f;
        }
    }

    void LateUpdate()
    {
        // Update the sorting order based on Y position
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y - offset) * -100) + 2560;
    }
}
