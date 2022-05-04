using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAggressiveState : BaseStateEnemies {

    private bool flee;
    private bool chase;

    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange) {
            enemy.aggro = 100f;
            if (Vector3.Distance(enemy.target.position, enemy.transform.position) < enemy.minRange) {
                //Debug.Log("flee");
                flee = true;
                enemy.agent.isStopped = false;
            }
            else {
                //Debug.Log("shoot");
                flee = false;          
                chase = false;
                enemy.agent.isStopped = true;
                isShootable(enemy);
            }      
        }
        else {
          //  Debug.Log("chase");
            chase = true;
            enemy.agent.isStopped = false;

            enemy.aggro -= 50f * Time.deltaTime;

            if (enemy.aggro < 0){
                enemy.aggro = 0;
                ExitState(enemy);
            }


        }
       


    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        //enemy.Animate();
        if (flee){
            Vector3 wolfPosition = enemy.transform.position;
            Vector3 playerPosition = enemy.target.transform.position;
            Vector3 direction = wolfPosition - playerPosition;
            Vector3 newPos = wolfPosition + direction;  
            enemy.agent.SetDestination(newPos);
        }

        if (chase){
            enemy.agent.SetDestination(enemy.target.position);
        }
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public void isShootable(EnemiesStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        // linha para atirar
        Vector3 direction = toPosition - fromPosition;

        direction.Normalize();
        enemy.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, Mathf.Infinity);
        if(hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player"){
           // Debug.Log("Shootable! Pew pew");
        }else{
            //Debug.Log(hit);
          //  Debug.Log("Encontrei o Jogador, mas tem um obstaculo na frente!");
        }
    }

    public void ExitState(EnemiesStateManager enemy){
        Debug.Log("Lost aggro on opponent");
        enemy.agent.isStopped = true;
        enemy.SwitchState(enemy.searchState);
    }
   
}
