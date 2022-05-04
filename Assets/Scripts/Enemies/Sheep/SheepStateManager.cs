using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateManager : EnemiesStateManager
{
    public Sprite[] idle;
    public Sprite[] run;
    public Sprite[] jump;

    public int count=0;
    public int index=0;
    int animationSpeed=20;

    public SheepAggressiveState aggressiveState = new SheepAggressiveState();

    public SheepTackleState tackleState = new SheepTackleState();
   
    public override void BecomeAggresive()
    {
        SwitchState(aggressiveState);
    }

    public override void Animate(){
        setDirection2();
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

        }else if(animationState.Equals("jump")){
            if(index>=2){
                index=0;
            }
            spriteRenderer.sprite = jump[index*4 + direction];
        }
        
    }

    public void setDirection2(){
       
        if(angle>-45 && angle<45){
            //right
            direction = 3;
        }else if(angle>45 && angle<135){
            //up
            direction = 2;
        }else if((angle>135 && angle>180)||(angle>-180 && angle<-135)){
            //left
            direction = 1;
        }else if(angle<-45 && angle>-135){
            //back
            direction = 0;
        }
    }
    public override void SetProperties(){
        health = 12f;
        damage = 20;

   }
    
}
