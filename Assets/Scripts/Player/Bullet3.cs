using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : Bullet
{
    public Sprite[] endAnimation;
    override public void SetStats(){
        speed = 15f;
        damage = 3;
        transform.localScale*=1.5f;
        direction = direction.normalized;
        rotateTowardsPlayer(direction);
    }

    override public IEnumerator Break(){
        if(!done){
            done = true;
            rb.velocity /=3;
            for(int i=0;i<endAnimation.Length;i++){
                spriteRenderer.sprite=endAnimation[i];
                yield return new WaitForSeconds(0.1f);
            }

            Destroy(spriteRenderer);
            GameObject explosionAnimation = (GameObject) Instantiate(explosion,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(explosionAnimation);
            Destroy(gameObject);
        }
    }

}
