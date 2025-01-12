using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Sprite initSprite;
    [SerializeField] private Sprite[] breakageSprites;
    [SerializeField] private Sprite[] prizes;
    [SerializeField] private float animationDuration;
    [SerializeField] private SpriteRenderer rewardSprite;
    public GameManager manager;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private int breakage = 0;
    private bool isAnimating = false;

    void Start()
    {
        Time.timeScale = 0.0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = initSprite;
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

        animator.SetTrigger("BreakTrigger");

        yield return new WaitForSecondsRealtime(animationDuration);

        breakage++;

        isAnimating = false;
    }

    private IEnumerator opened()
    {
        isAnimating = true;

        animator.SetTrigger("OpenTrigger");

        yield return new WaitForSecondsRealtime(animationDuration);

        int prizeNum = Random.Range(0, prizes.Length);
        spriteRenderer.sprite = prizes[prizeNum];
        manager.prizeReceived(prizeNum);

        Time.timeScale = 1.0f;
    }
}
