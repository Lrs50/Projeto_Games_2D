using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossNormalAttackState: BaseStateBoss {
    int counter=0;
    bool shoot = true;
    int animationType;
    ProjectileAttackTemplate data;
    public override void EnterState(BossStateManager enemy){
        shoot= true;
        counter=0;
        animationType = Random.Range(0,2);
        enemy.normalBulletProperties._bossPosition = enemy.transform.position;
        enemy.normalBulletProperties._numberOfProjectiles = Random.Range(2,6);
        data = new ProjectileAttackTemplate(enemy.normalBulletProperties._projectilePrefab,enemy.normalBulletProperties._numberOfProjectiles,enemy.normalBulletProperties._projectileSpeed,enemy.normalBulletProperties._spawnRadius);
    }

    public override void UpdateState(BossStateManager enemy){
        //rotateTowardsPlayer(enemy);
    }

    public override void FixedUpdateState(BossStateManager enemy){

        if(counter>=data.Number && enemy.index==0){
            if(enemy.audioSource.isPlaying){
                enemy.audioSource.Stop();
            }
            enemy.SwitchState(enemy.searchState);
        }

        if(enemy.index>=enemy.simpleAttackAnimation1.Length){
            enemy.index=0;
            shoot=true;
        }
        if(enemy.indexWings>=enemy.idleWingsAnimation.Length){
            enemy.indexWings = 0;
        }
        enemy.wingsSR.sprite = enemy.idleWingsAnimation[enemy.index];


        if(enemy.index==5 && shoot){
            shoot=false;
            enemy.audioSource.clip = enemy.rangedAttackSound;
            enemy.audioSource.Play();
            GameObject tempBullet = GameObject.Instantiate(data.Prefab, enemy.transform.position, enemy.transform.rotation) as GameObject;    
            counter++;
        }
        if(animationType==0){
            enemy.spriteRenderer.sprite = enemy.simpleAttackAnimation1[enemy.index];
        }else{
            enemy.spriteRenderer.sprite = enemy.simpleAttackAnimation2[enemy.index];
        }
        
    }

    public override void OnCollisionEnter(BossStateManager enemy){
    }

}