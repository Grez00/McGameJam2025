using UnityEngine;
using System.Collections;

public class LootTest : MonoBehaviour
{
    private LootBoxReward[] prizes;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        prizes = new LootBoxReward[2];
        prizes[0] = new FreeCat();
        prizes[1] = new Cat2();

        spriteRenderer = GetComponent<SpriteRenderer>();

        LootBoxReward prize = prizes[Random.Range(0, prizes.Length)];
        spriteRenderer.sprite = prize.getSprite();
        prize.received();
    }
}
