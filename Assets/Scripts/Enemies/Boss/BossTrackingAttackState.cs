using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossTrackingAttackState: BaseStateBoss {
        int counter=0;
        bool shoot = true;
        int animationType;
        ProjectileAttackTemplate data;
    public override void EnterState(BossStateManager enemy){
        shoot= true;
        counter=0;
        animationType = Random.Range(0,2);
        enemy.trackingBulletProperties._bossPosition = enemy.transform.position;
        data = new ProjectileAttackTemplate(enemy.trackingBulletProperties._projectilePrefab,enemy.trackingBulletProperties._numberOfProjectiles,enemy.trackingBulletProperties._projectileSpeed,enemy.trackingBulletProperties._spawnRadius);
    }

    public override void UpdateState(BossStateManager enemy){

        if(enemy.instanceOfTrackingBullet && !shoot){
            enemy.instanceOfTrackingBullet.transform.position = Vector2.MoveTowards(enemy.instanceOfTrackingBullet.transform.position, enemy.target.transform.position, enemy.trackingBulletProperties._projectileSpeed * Time.deltaTime);
            Vector2 direction = (enemy.instanceOfTrackingBullet.transform.position - enemy.target.transform.position).normalized;
            var angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            var offset = 90f;
            enemy.instanceOfTrackingBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }else if(!shoot){
            if(enemy.audioSource.isPlaying){
                enemy.audioSource.Stop();
            }
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){
        if(enemy.index>=enemy.simpleAttackAnimation1.Length){
            enemy.index=0;
        }

        if(enemy.index==5 && shoot){
            enemy.instanceOfTrackingBullet = SpawnProjectiles(data,enemy); 
            shoot=false;
            counter++;
        }
        if(enemy.indexWings>=enemy.idleWingsAnimation.Length){
            enemy.indexWings = 0;
        }
        enemy.wingsSR.sprite = enemy.idleWingsAnimation[enemy.index];
        if(animationType==0 && shoot){
            enemy.spriteRenderer.sprite = enemy.simpleAttackAnimation1[enemy.index];
        }else if(shoot){
            
            enemy.spriteRenderer.sprite = enemy.simpleAttackAnimation2[enemy.index];
        }
    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    public GameObject SpawnProjectiles(ProjectileAttackTemplate shotData,BossStateManager enemy){
        enemy.audioSource.clip = enemy.rangedAttackSound;
        enemy.audioSource.Play();
        GameObject tempBullet = GameObject.Instantiate(shotData.Prefab, enemy.trackingBulletProperties._bossPosition, Quaternion.identity) as GameObject;
        return tempBullet;
    }
}