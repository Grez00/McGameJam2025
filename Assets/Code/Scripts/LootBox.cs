using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private int breakage = 0;
    public Sprite sprite0;
    public Sprite[] breakageSprites;
    public Sprite[] openedSprites;
    public Animator animator;
    public float animationDuration;

    private SpriteRenderer spriteRenderer;
    private bool isAnimating = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = sprite0;
    }

    public void clicked()
    {
        Debug.Log("CLICKED");

        if (isAnimating) return;

        else
        {
            if (breakage < 6)
            {
                StartCoroutine(Breakage());
            }
            else
            {
                StartCoroutine(opened());
            }
        }
    }

    private IEnumerator Breakage()
    {
        isAnimating = true;

        animator.SetTrigger("BreakTrigger");  // I DO NOT UNDERSTAND

        yield return new WaitForSeconds(animationDuration);

        breakage++;
        /*if (breakage < breakageSprites.Length)
        {
            spriteRenderer.sprite = breakageSprites[breakage];
        }*/

        isAnimating = false;
    }

    private IEnumerator opened()
    {
        isAnimating = true;

        animator.SetTrigger("OpenTrigger"); // SAVE ME

        yield return new WaitForSeconds(animationDuration);

        spriteRenderer.sprite = openedSprites[Random.Range(0, openedSprites.Length)];
    }
}
