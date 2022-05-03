using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossNormalAttackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.normalBulletProperties._bossPosition = enemy.transform.position;
        ProjectileAttackTemplate data = new ProjectileAttackTemplate(enemy.normalBulletProperties._projectilePrefab,enemy.normalBulletProperties._numberOfProjectiles,enemy.normalBulletProperties._projectileSpeed,enemy.normalBulletProperties._spawnRadius);
        enemy.StartCoroutine(defaultProjectile(data,enemy));
    }

    public override void UpdateState(BossStateManager enemy){
        rotateTowardsPlayer(enemy);
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){
    }
    private IEnumerator defaultProjectile(ProjectileAttackTemplate shotData,BossStateManager enemy){
        for (int i = 0; i < shotData.Number; i++)
        {
            GameObject tempBullet = GameObject.Instantiate(shotData.Prefab, enemy.transform.position, enemy.transform.rotation) as GameObject;           
            yield return new WaitForSeconds(1f);
        }
        enemy.SwitchState(enemy.searchState);
    }
}