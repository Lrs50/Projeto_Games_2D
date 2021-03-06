using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedStateManager : EnemiesStateManager
{

    DeformedAggressiveState aggressiveState = new DeformedAggressiveState();
    
    public GameObject Projectile;
    public Sprite[] idle;
    public Sprite[] attack;
    
    
    public int count=0;
    Transform shootingOrigin;

    public override void BecomeAggresive()
    {
        shootingOrigin = reference.GetChild(1); 
        SwitchState(aggressiveState);
    }

    public override void Animate(){
        setDirection();

        if(animationState.Equals("idle")){
            
            spriteRenderer.sprite = idle[direction];

        }else if(animationState.Equals("attack")){
            count = aggressiveState.count;
            float timePerFrame = aggressiveState.shootAnimationTime/4;
            int index = Mathf.FloorToInt(count/timePerFrame)*4 + direction;
            
            spriteRenderer.sprite = attack[index];
        }
        
    }

    public override void OnShoot(Vector3 direction) {
        GameObject bulletTemp =Instantiate(Projectile,shootingOrigin.position,Quaternion.identity);
        BulletEnemy scriptTemp = bulletTemp.GetComponent<BulletEnemy>();
        scriptTemp.setDestination((Vector2) direction);
        audioSource.clip = attackSound;
        audioSource.Play();

    }

    public override void SetProperties(){
        health = 5f;
        damage = 0;

   }

}
