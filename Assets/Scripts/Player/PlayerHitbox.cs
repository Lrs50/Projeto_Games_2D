using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public PlayerStateManager player;
    float damageValue = 10;

    private  void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"{other.gameObject.tag} {damageValue}");
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile")){
            player.TakeDamage(damageValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.gameObject.tag} {damageValue}");
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Projectile")){
            Debug.Log($"{other.gameObject.tag} {damageValue}");
            player.TakeDamage(damageValue);
        }
    }
}
