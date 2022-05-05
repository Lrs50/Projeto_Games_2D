using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStateManager : EnemiesStateManager
{

    public WolfAggressiveState aggressiveState = new WolfAggressiveState();
    public Sprite[] idle;
    public Sprite[] attack;
    Transform shootingOrigin;
    public int count=0;
    public int index=0;
    int animationSpeed=10;
    

    public override void BecomeAggresive()
    {
        shootingOrigin = reference.GetChild(1); 
        SwitchState(aggressiveState);
    }

    public override void SetProperties(){
        health = 10f;
        damage = 0;

   }
    public override void Animate(){
        setDirection2();

        count++;
        if(count>=animationSpeed){
            count=0;
            index++;
        }

        if(animationState.Equals("idle")){
            if(index>=Mathf.Floor((idle.Length)/4)){
                index=0;
            }
            spriteRenderer.sprite = idle[index*4+direction];
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
}
