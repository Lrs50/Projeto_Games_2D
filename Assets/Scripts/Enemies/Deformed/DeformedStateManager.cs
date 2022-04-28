using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedStateManager : EnemiesStateManager
{

    DeformedAggressiveState aggressiveState = new DeformedAggressiveState();
    
    public Sprite[] iddle;
    public Sprite[] attack;

    public int direction = 0;
    public int count=0;
    Transform shootingOrigin;

    public override void BecomeAggresive()
    {
        shootingOrigin = reference.GetChild(0); 
        SwitchState(aggressiveState);
    }

    public override void Animate(){
        setDirection();

        if(animationState.Equals("iddle")){
            
            spriteRenderer.sprite = iddle[direction];

        }else if(animationState.Equals("attack")){
            count = aggressiveState.count;
            float timePerFrame = aggressiveState.shootAnimationTime/4;
            int index = Mathf.FloorToInt(count/timePerFrame)*4 + direction;
            
            spriteRenderer.sprite = attack[index];
        }
        
    }

    private void setDirection(){
        if(angle>-90 && angle<30){
            //front right
            direction = 1;
        }else if(angle<-90 && angle>-180){
            //front left
            direction = 0;
        }else if(angle<180 && angle>90){
            //back left
            direction = 2;
        }else if(angle<90 && angle>30){
            direction = 3;
        }
    }

}
