using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DepthSorter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Update the sorting order based on Y position
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100) + 2560;
    }
}
