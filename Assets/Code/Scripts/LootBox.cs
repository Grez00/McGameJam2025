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
        spriteRenderer.sprite = sprite0;
    }

    public void clicked()
    {
        Debug.Log("CLICKED");

        if (isAnimating)
            return;

        if (breakage < breakageSprites.Length)
        {
            StartCoroutine(HandleBreakage());
        }
        else if (breakage >= breakageSprites.Length)
        {
            StartCoroutine(opened());
        }
    }

    private IEnumerator HandleBreakage()
    {
        isAnimating = true;

        //animator.SetTrigger("BreakageTrigger"); I DO NOT UNDERSTAND

        yield return new WaitForSeconds(animationDuration);

        breakage++;
        if (breakage < breakageSprites.Length)
        {
            spriteRenderer.sprite = breakageSprites[breakage];
        }

        isAnimating = false;
    }

    private IEnumerator opened()
    {
        isAnimating = true;

        //animator.SetTrigger("OpenTrigger"); SAVE ME

        yield return new WaitForSeconds(animationDuration);

        spriteRenderer.sprite = openedSprites[Random.Range(0, openedSprites.Length)];
    }
}
