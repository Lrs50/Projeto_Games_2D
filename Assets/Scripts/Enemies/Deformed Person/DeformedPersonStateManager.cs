using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedPersonStateManager : EnemiesStateManager
{
    public DeformedPersonAggressiveState aggresiveState {get; protected set;} = new DeformedPersonAggressiveState();
    public Sprite[] run;
    public Sprite[] idle;
    
    public int count=0;
    public int index=0;
    int animationSpeed=10;

    public override void BecomeAggresive()
    {
        animationState="run";
        SwitchState(aggresiveState);
    }

    public override void Animate(){
        setDirection();
        
        count++;
        if(count>=animationSpeed){
            count=0;
            index++;
        }
        if(index>=4){
            index=0;
        }

        if(animationState.Equals("idle")){
            
            spriteRenderer.sprite = idle[direction];

        }else if(animationState.Equals("run")){
            
            spriteRenderer.sprite = run[index*4 + direction];
        }
        
    }

    public override void SetProperties(){
        health = 3f;
        damage = 11;

    }

}
