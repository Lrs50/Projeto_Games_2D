using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossTrackingAttackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.trackingBulletProperties._bossPosition = enemy.transform.position;
        ProjectileAttackTemplate data = new ProjectileAttackTemplate(enemy.trackingBulletProperties._projectilePrefab,enemy.trackingBulletProperties._numberOfProjectiles,enemy.trackingBulletProperties._projectileSpeed,enemy.trackingBulletProperties._spawnRadius);
        enemy.instanceOfTrackingBullet = SpawnProjectiles(data,enemy);
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.instanceOfTrackingBullet){
            enemy.instanceOfTrackingBullet.transform.position = Vector2.MoveTowards(enemy.instanceOfTrackingBullet.transform.position, enemy.target.transform.position, enemy.trackingBulletProperties._projectileSpeed * Time.deltaTime);
            Vector2 direction = (enemy.instanceOfTrackingBullet.transform.position - enemy.target.transform.position).normalized;
            var angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            var offset = 90f;
            enemy.instanceOfTrackingBullet.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }else{
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    public GameObject SpawnProjectiles(ProjectileAttackTemplate shotData,BossStateManager enemy){
        GameObject tempBullet = GameObject.Instantiate(shotData.Prefab, enemy.trackingBulletProperties._bossPosition, Quaternion.identity) as GameObject;
        return tempBullet;
    }
}