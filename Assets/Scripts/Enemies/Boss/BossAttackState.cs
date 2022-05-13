using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAttackState: BaseStateBoss {
    ProjectileAttackTemplate data;
    public override void EnterState(BossStateManager enemy){
        enemy.bulletProperties._bossPosition = enemy.transform.position;
        data = new ProjectileAttackTemplate(enemy.bulletProperties._projectilePrefab,enemy.bulletProperties._numberOfProjectiles,enemy.bulletProperties._projectileSpeed,enemy.bulletProperties._spawnRadius);
        
    }

    public override void UpdateState(BossStateManager enemy){
    }

    public override void FixedUpdateState(BossStateManager enemy){
        if(enemy.index>=enemy.Animation360.Length){
            enemy.index=0;            
            SpawnProjectiles(data,enemy);
        }
        if(!enemy.audioSource.isPlaying){
            enemy.audioSource.clip = enemy.loadingSound;
            enemy.audioSource.Play();
        }
        if(enemy.indexWings>=enemy.Animation360Wings.Length){
            enemy.indexWings = 0;
        }
        enemy.wingsSR.sprite = enemy.Animation360Wings[enemy.index];
        enemy.spriteRenderer.sprite = enemy.Animation360[enemy.index];

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    public void SpawnProjectiles(ProjectileAttackTemplate shotData,BossStateManager enemy){
        enemy.audioSource.clip = enemy.rangedAttackSound;
        enemy.audioSource.Play();
        float angleStep = 360f / shotData.Number;
        float angle = 0f;
        float transformUpAngle = Mathf.Atan2(enemy.transform.up.x, enemy.transform.up.y);
        float PIx2 = Mathf.PI * 2;

        for (int i = 0; i < shotData.Number; i++)
        {

            Vector2 startPosition = new Vector2(Mathf.Sin(((angle*Mathf.PI)/180)+transformUpAngle),Mathf.Cos(((angle*Mathf.PI)/180)+transformUpAngle));

            Vector2 relativeStartPosition = (Vector2)enemy.bulletProperties._bossPosition + startPosition * shotData.Radius;
            float rotationZ = (360 - angle) - (angle * PIx2 + transformUpAngle) * Mathf.Rad2Deg;
            Vector2 shotMovementVector = (relativeStartPosition - (Vector2)enemy.bulletProperties._bossPosition).normalized * shotData.Speed;

            GameObject tempBullet = GameObject.Instantiate(shotData.Prefab, relativeStartPosition, Quaternion.Euler(0,0,rotationZ)) as GameObject;
            tempBullet.GetComponent<Rigidbody2D>().velocity = shotMovementVector;

            angle += angleStep;
        }        
        enemy.SwitchState(enemy.searchState);
    }
}