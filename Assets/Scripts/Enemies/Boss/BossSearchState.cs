using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSearchState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.rb.velocity = Vector2.zero;
        enemy.agent.speed = enemy.searchSpeed;
        enemy.timerForAttacks = 0;
    }

    public override void UpdateState(BossStateManager enemy){
        //rotateTowardsPlayer(enemy);
        float dist = Vector3.Distance(enemy.transform.position,enemy.target.transform.position);
        if(dist > 5f){
            followPlayer(enemy);
            //enemy.agent.SetDestination(enemy.target.position);
        }else if(dist < 4.95f){
            Vector3 flee = (enemy.target.transform.position - enemy.transform.position).normalized;
            followPoint(enemy,flee);
            //enemy.agent.SetDestination((enemy.target.transform.position - enemy.transform.position).normalized * 5);
        }
        enemy.timerForAttacks += Time.deltaTime;
        if(enemy.timerForAttacks > 5){
            int whichAttack = 3;//Random.Range(1,4);
            Debug.Log(whichAttack);
            if(whichAttack <= 1){
                enemy.SwitchState(enemy.attackState);
            }else if(whichAttack > 1 && whichAttack <= 2){
                enemy.SwitchState(enemy.flyingState);
            }else if(whichAttack > 2 && whichAttack <= 3){
                enemy.SwitchState(enemy.dashAttack);
            }else if (whichAttack > 2 && whichAttack <= 4){
                enemy.SwitchState(enemy.trackAttack);
            }else if (whichAttack > 3 && whichAttack <= 5){
                enemy.SwitchState(enemy.normalAttack);
            }else{
                enemy.SwitchState(enemy.drillAttack);
            }
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    public void followPlayer(BossStateManager enemy){
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);
    }
    public void followPoint(BossStateManager enemy,Vector3 flee){
        enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.transform.position - flee, enemy.baseSpeed * Time.deltaTime);
        //enemy.transform.position -= flee * (enemy.baseSpeed +speed) * Time.deltaTime;
    }
}