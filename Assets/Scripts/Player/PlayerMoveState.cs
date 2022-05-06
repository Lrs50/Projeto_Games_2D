using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerMoveState : BaseStatePlayer {
    Vector2 previousDirection;
    float canRun = 1f;
    float staminaCost = 25f;
    float dashCooldown = 0.5f;
    float dashCounter = 1;

    public override void EnterState(PlayerStateManager player) {
    	player.numFrames = 4;
        previousDirection = player.walkInput;
    }

    public override void UpdateState(PlayerStateManager player) {

        if(player.dashInput!=0f && player.stamina>=staminaCost && dashCounter>=1){
            player.stamina -= staminaCost;
            dashCounter = 0;
            player.SwitchState(player.dashState);
        }

        if(player.sprintInput!=0f && player.stamina>0){
            canRun = 1;
            
        }else{
            canRun = 0;
            player.sprintInput=0f;
        }

    }

    public override void FixedUpdateState(PlayerStateManager player){



        if(dashCounter<1){
            dashCounter+= 1/(50f*dashCooldown);
        }

        if(canRun==1){
            player.numFrames=4;
            if(player.animationFrame>=player.numFrames) player.animationFrame=0;
            player.spriteRenderer.sprite = player.runAnimation[player.animationOrientation + player.animationFrame*4];
            player.wingsSR.sprite = player.wingsAnimation[player.animationOrientation + 4];
            player.stamina -= 0.25f; 
        }else{
            
            if(player.attackFlag){
                player.numFrames=8;
                if(player.animationFrame>=player.numFrames) player.animationFrame=0;
                player.spriteRenderer.sprite = player.walkAttackAnimation[player.animationOrientation + player.animationFrame*4];
            }else{
                player.numFrames=4;
                if(player.animationFrame>=player.numFrames) player.animationFrame=0;
                    
                player.spriteRenderer.sprite = player.walkAnimation[player.animationOrientation + player.animationFrame*4];
            }
            player.wingsSR.sprite = player.wingsAnimation[player.animationOrientation];
            player.stamina+= 50f/200f; // 4sec
        }
    
        Vector2 direction = player.walkInput;

        if(previousDirection!=direction){
            previousDirection = direction;
            player.rb.velocity=direction*player.rb.velocity.magnitude;
        }

        if (direction == Vector2.zero){
            ExitState(player);
        	player.SwitchState(player.idleState);
            return;
        }
        player.rb.AddForce(direction*player.baseSpeed*(1+canRun));

        player.rb.velocity = Vector2.ClampMagnitude(player.rb.velocity,player.maxSpeed*(1+canRun*player.sprintSpeed));
        //Debug.Log(player.rb.velocity);

        
		
    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

    public void ExitState(PlayerStateManager player){
        player.rb.velocity = Vector2.zero;
    }
   
}