using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerStateManager player;
    float damageValue = 0;

    private  void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile") || other.gameObject.tag.Equals("Boss") ||
         other.gameObject.tag.Equals("NormalFeather") || other.gameObject.tag.Equals("TrackingFeather") || other.gameObject.tag.Equals("Feather360")){
            if(other.gameObject.tag.Equals("Enemy")){
                
                EnemiesStateManager enemy = other.gameObject.GetComponent<EnemiesStateManager>();
                damageValue = enemy.damage;
            }
            if(other.gameObject.tag.Equals("Boss")){
                BossStateManager boss = other.gameObject.GetComponent<BossStateManager>();
                damageValue = boss.damage;
            }
            if(other.gameObject.tag.Equals("Projectile")){
                BulletEnemy enemy = other.gameObject.GetComponent<BulletEnemy>();
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("NormalFeather")){
                NormalProjectile enemy = other.gameObject.GetComponent<NormalProjectile>();
                Debug.Log(enemy.damage);
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("TrackingFeather")){
                TrackingProjectile enemy = other.gameObject.GetComponent<TrackingProjectile>();
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("Feather360")){
                FeatherProjectile enemy = other.gameObject.GetComponent<FeatherProjectile>();
                damageValue = enemy.damage;
                
            }
            
            player.TakeDamage(damageValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{other.gameObject.tag} {damageValue}");
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile") || other.gameObject.tag.Equals("Boss") ||
         other.gameObject.tag.Equals("NormalFeather") || other.gameObject.tag.Equals("TrackingFeather") || other.gameObject.tag.Equals("Feather360")){
            if(other.gameObject.tag.Equals("Enemy")){
                
                EnemiesStateManager enemy = other.gameObject.GetComponent<EnemiesStateManager>();
                damageValue = enemy.damage;
            }
            if(other.gameObject.tag.Equals("Projectile")){
                BulletEnemy enemy = other.gameObject.GetComponent<BulletEnemy>();
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("Boss")){
                BossStateManager boss = other.gameObject.GetComponent<BossStateManager>();
                damageValue = boss.damage;
            }
            if(other.gameObject.tag.Equals("NormalFeather")){
                NormalProjectile enemy = other.gameObject.GetComponent<NormalProjectile>();
                Debug.Log(enemy.damage);
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("TrackingFeather")){
                TrackingProjectile enemy = other.gameObject.GetComponent<TrackingProjectile>();
                damageValue = enemy.damage;
                
            }
            if(other.gameObject.tag.Equals("Feather360")){
                FeatherProjectile enemy = other.gameObject.GetComponent<FeatherProjectile>();
                damageValue = enemy.damage;
                
            }
            
            player.TakeDamage(damageValue);
        }
    }
}
