using System.Collections;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Sprite initSprite;
    [SerializeField] private Sprite[] breakageSprites;
    [SerializeField] private Sprite[] prizes;
    [SerializeField] private float animationDuration = 0.5f;
    public GameManager manager;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private int breakage = 0;
    private bool isAnimating = false;

    void Start()
    {
        Time.timeScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = initSprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clicked();
        }
    }


    public void clicked()
    {
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

        yield return new WaitForSecondsRealtime(animationDuration);

        breakage++;

        isAnimating = false;
    }

    private IEnumerator opened()
    {
        isAnimating = true;

        animator.SetTrigger("OpenTrigger");

        yield return new WaitForSecondsRealtime(animationDuration);

        int rand = Random.Range(0, prizes.Length);
        Sprite prizeSprite = prizes[rand];
        spriteRenderer.sprite = prizeSprite;
        manager.gacha(rand);

        Time.timeScale = 1;
    }
}
