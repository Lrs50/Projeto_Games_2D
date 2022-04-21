using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadingProgressBar : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
 
    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Loader.GetLoadingProgress()>0.1){
            spriteRenderer.sprite = sprites[0];
        }else if(Loader.GetLoadingProgress()>0.25){
            spriteRenderer.sprite = sprites[1];
        }else if(Loader.GetLoadingProgress()>0.5){
            spriteRenderer.sprite = sprites[2];
        }else{
            spriteRenderer.sprite = sprites[3];
        }

    }
}
