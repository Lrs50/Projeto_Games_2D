using UnityEngine;
using UnityEngine.UI;
using System;
public class EnemiesMoveState : BaseStateEnemies {

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
            
            Debug.Log(enemy.aggro); 
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        rotateTowardsPlayer(enemy);
        enemy.agent.SetDestination(enemy.target.position);
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy) {

    }

    public void ExitState(EnemiesStateManager enemy){
        Debug.Log("Lost aggro on opponent");
        enemy.agent.isStopped = true;
        enemy.SwitchState(enemy.searchState);
    }

    public virtual void followPlayer(EnemiesStateManager enemy){        
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);
    }

    public virtual void rotateTowardsPlayer(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public virtual void checkObstacles(EnemiesStateManager enemy){
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position,enemy.transform.forward,20,enemy.obstacles.value);
        if(hit.collider != null && hit.collider.transform != enemy.transform && hit.collider.tag != "Player"){
            Debug.DrawLine(enemy.transform.position, hit.point, Color.red);
            Debug.Log(hit.collider.tag);
        }
    }
   
}