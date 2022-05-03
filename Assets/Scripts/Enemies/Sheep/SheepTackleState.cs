using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTackleState : BaseStateEnemies
{

    public float dashMag = 25f;
    public float dashTimer = 0.5f;

    public override void EnterState(EnemiesStateManager enemy){
        enemy.animationState="jump";
        enemy.waitTime = enemy.startWaitTime / 2;
    }

    public override void UpdateState(EnemiesStateManager enemy){
        if (enemy.waitTime <= 0){
            enemy.waitTime = enemy.startWaitTime;
            enemy.StartCoroutine(Dash(enemy));
        }
        else {
            enemy.waitTime -= Time.deltaTime;
        }

        if(Vector3.Distance(enemy.target.position, enemy.transform.position) > enemy.minRange + 1.5f){
            ExitState(enemy);
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        enemy.getAngle(enemy);
        enemy.Animate();
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }
    
    private IEnumerator Dash(EnemiesStateManager enemy){
        enemy.getAngle(enemy);
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        direction.Normalize();
        enemy.rb.AddForce(direction * dashMag, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTimer);
    } 

    public void rotateTowardsPlayer(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void ExitState(EnemiesStateManager enemy){
        enemy.BecomeAggresive();
    }
    
}
