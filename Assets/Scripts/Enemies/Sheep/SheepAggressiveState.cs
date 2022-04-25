using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAggressiveState : BaseStateEnemies
{
    public override void EnterState(EnemiesStateManager enemy) {
        enemy.agent.isStopped = false;
    }

    public override void UpdateState(EnemiesStateManager enemy) {
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) > enemy.maxRange){
            enemy.aggro -= 100f * Time.deltaTime;

            if (enemy.aggro < 0){
                enemy.aggro = 0;
                ExitState(enemy);
            }
        }
            
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) < enemy.minRange){
            enemy.agent.isStopped = true;
            
            // foi mal por isso aqui, tarde demais pra ajeitar a arquitetura
            enemy.SwitchState(new SheepTackleState());
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        rotateTowardsPlayer(enemy);
        enemy.agent.SetDestination(enemy.target.position);
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy) {

    }

    public void ExitState(EnemiesStateManager enemy){
        //Debug.Log("Lost aggro on opponent");
        enemy.SwitchState(enemy.searchState);
    }

    public void rotateTowardsPlayer(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

}
