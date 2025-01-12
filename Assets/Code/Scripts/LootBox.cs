using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Sprite initSprite;
    [SerializeField] private Sprite[] breakageSprites;
    [SerializeReference] private LootBoxReward[] prizes = new LootBoxReward[2];
    [SerializeField] private float animationDuration;
    [SerializeField] private SpriteRenderer rewardSprite;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private int breakage = 0;
    private bool isAnimating = false;

    void Start()
    {
        prizes[0] = new FreeCat();
        prizes[1] = new Cat2();

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
            if (breakage < 3)
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

        yield return new WaitForSeconds(animationDuration);

        breakage++;

        isAnimating = false;
    }

    private IEnumerator opened()
    {
        isAnimating = true;

        animator.SetTrigger("OpenTrigger");

        yield return new WaitForSeconds(animationDuration);

        LootBoxReward prize = prizes[Random.Range(0, prizes.Length)];
        rewardSprite.sprite = prize.getSprite();
        prize.received();
        this.gameObject.SetActive(false);
    }
}
