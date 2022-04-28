using UnityEngine;
using UnityEngine.UI;
using System;
public class EnemiesMoveState : BaseStateEnemies {


    public override void EnterState(EnemiesStateManager enemy) {
    	//player.rb.MovePosition(player.rb.position + player.walkInput * (player.baseSpeed + (player.sprintInput * player.sprintSpeed)) * Time.fixedDeltaTime);
    }

    public override void UpdateState(EnemiesStateManager enemy) {
        //checkObstacles(enemy);
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange){
            followPlayer(enemy);
            rotateTowardsPlayer(enemy);            
        }else{
            // enemy.rb.velocity = Vector2.zero;
            // enemy.rb.angularVelocity = 0f;
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
		//em direção ao player
        //transform
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy) {

    }

    public virtual void ExitState(EnemiesStateManager enemy){
        enemy.rb.velocity = Vector2.zero;
        enemy.rb.angularVelocity = 0f;
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