using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    public Sprite[] arrowSet;
    public SpriteRenderer spriteRenderer;
    private int frameIndex = 0;
    private int count = 0;
    private int animationTime = 7;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        count++;
        if (count >= animationTime){
            count = 0;
            frameIndex++;
            if (frameIndex >= 2)
                frameIndex = 0;
        }
        spriteRenderer.sprite = arrowSet[frameIndex];
    }
}
