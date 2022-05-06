using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAggressiveState : BaseStateEnemies {

    private bool flee;
    private bool chase;

    private bool shoot;
    int count =0;
    int shootTimer = 50;
    private bool delay = true;
    private Vector3 shootDirection;

    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange) {
            enemy.aggro = 100f;
            if (Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.minRange) {
                //Debug.Log("flee");
                enemy.animationState="idle";
                Flee();
            }
            else {
                Debug.Log("shoot");
                enemy.animationState="attack";
                Stop();
                isShootable(enemy);
            }      
        }
        else {
          //  Debug.Log("chase");
            enemy.animationState="idle";
            Chase();

            enemy.aggro -= 50f * Time.deltaTime;

            if (enemy.aggro < 0){
                enemy.aggro = 0;
                ExitState(enemy);
            }
        }
    }

    public void Chase() {
        chase = true;
        flee = false;
        shoot = false;
    }

    public void Flee() {
        flee = true;
        chase = false;
        shoot = false;
    }

    public void Stop(){
        flee = false;
        chase = false;
        shoot = false;
    }

    public void Shoot(){
        flee = false;
        chase = false;
        shoot = true;
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        enemy.Animate();
        if(shoot){
            count++;
            if(count>=shootTimer){
                count=0;
                delay=false;
            }
        }
        if (flee){
            enemy.agent.isStopped = false;
            Vector3 wolfPosition = enemy.transform.position;
            Vector3 playerPosition = enemy.target.transform.position;
            Vector3 direction = wolfPosition - playerPosition;
            Vector3 newPos = wolfPosition + direction;  
            enemy.agent.SetDestination(newPos);
        }
        else if (chase){
            enemy.agent.isStopped = false;
            enemy.agent.SetDestination(enemy.target.position);
        }
        else if (shoot && !delay){
            enemy.animationState="attack";
            enemy.agent.isStopped = true;
            delay=true;
            enemy.OnShoot(shootDirection);
        }
        else {
            enemy.agent.isStopped = true;
        }
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public void isShootable(EnemiesStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        // linha para atirar
        shootDirection = toPosition - fromPosition;

        shootDirection.Normalize();
        enemy.angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg; 

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, shootDirection, Mathf.Infinity);
        if(hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player"){
           // Debug.Log("Shootable! Pew pew");
            Debug.DrawLine(toPosition, fromPosition);
            Stop();
            Shoot();
        }
        else{
            //Debug.Log(hit);            
            Chase();   
        }
    }

    public void ExitState(EnemiesStateManager enemy){
        Debug.Log("Lost aggro on opponent");
        enemy.agent.isStopped = true;
        enemy.SwitchState(enemy.searchState);
    }
   
}
