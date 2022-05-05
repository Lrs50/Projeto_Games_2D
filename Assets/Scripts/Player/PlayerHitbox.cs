using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerStateManager player;
    float damageValue = 0;

    private  void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile")){
            if(other.gameObject.tag.Equals("Enemy")){
                
                EnemiesStateManager enemy = other.gameObject.GetComponent<EnemiesStateManager>();
                damageValue = enemy.damage;
            }
            if(other.gameObject.tag.Equals("Projectile")){
                BulletEnemy enemy = other.gameObject.GetComponent<BulletEnemy>();
                damageValue = enemy.damage;
                
            }
            
            player.TakeDamage(damageValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{other.gameObject.tag} {damageValue}");
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile")){
            if(other.gameObject.tag.Equals("Enemy")){
                
                EnemiesStateManager enemy = other.gameObject.GetComponent<EnemiesStateManager>();
                damageValue = enemy.damage;
            }
            if(other.gameObject.tag.Equals("Projectile")){
                BulletEnemy enemy = other.gameObject.GetComponent<BulletEnemy>();
                damageValue = enemy.damage;
                
            }
            
            player.TakeDamage(damageValue);
        }
    }
}
