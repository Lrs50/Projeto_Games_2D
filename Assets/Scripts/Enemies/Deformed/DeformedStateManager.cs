using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedStateManager : EnemiesStateManager
{

    DeformedAggressiveState aggressiveState = new DeformedAggressiveState();
    
    public Sprite[] fowardSprite;
    public Sprite[] backSprite;


    public override void BecomeAggresive()
    {
        SwitchState(aggressiveState);
    }

    public override void Animate(){
        
        if(angle>-90 && angle<30){
            //front right
            spriteRenderer.sprite = fowardSprite[0];
        }else if(angle<-90 && angle>-180){
            //front left
            spriteRenderer.sprite = fowardSprite[1];
        }else if(angle<180 && angle>90){
            //back left
            spriteRenderer.sprite = backSprite[1];
        }else if(angle<90 && angle>30){
            //back right
            spriteRenderer.sprite = backSprite[0];
        }
    }

}
