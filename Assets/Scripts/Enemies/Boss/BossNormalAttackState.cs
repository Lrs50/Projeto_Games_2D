using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossNormalAttackState: BaseStateBoss {
    private Vector3 currentPlayerPos;
    public override void EnterState(BossStateManager enemy){
        enemy.normalBulletProperties._bossPosition = enemy.transform.position;
        currentPlayerPos = enemy.target.transform.position;
        ProjectileAttackTemplate data = new ProjectileAttackTemplate(enemy.normalBulletProperties._projectilePrefab,enemy.normalBulletProperties._numberOfProjectiles,enemy.normalBulletProperties._projectileSpeed,enemy.normalBulletProperties._spawnRadius);
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.instanceOfNormalBullet){
            //enemy.instanceOfNormalBullet.transform.position = Vector2.MoveTowards(enemy.instanceOfNormalBullet.transform.position, currentPlayerPos, enemy.normalBulletProperties._projectileSpeed * Time.deltaTime);
            enemy.instanceOfNormalBullet.transform.position += (currentPlayerPos - enemy.instanceOfNormalBullet.transform.position).normalized *10 * Time.deltaTime;
        }else{
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
}