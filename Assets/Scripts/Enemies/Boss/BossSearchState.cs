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
        rotateTowardsPlayer(enemy);
        enemy.agent.SetDestination(enemy.target.position);
        enemy.timerForAttacks += Time.deltaTime;
        if(enemy.timerForAttacks > 5){
            int whichAttack = 2;//Random.Range(1,4);
            Debug.Log(whichAttack);
            if(whichAttack <= 1){
                enemy.SwitchState(enemy.attackState);
            }else if(whichAttack > 1 && whichAttack <= 2){
                enemy.SwitchState(enemy.flyingState);
            }else if(whichAttack > 2 && whichAttack <= 3){
                enemy.SwitchState(enemy.dashAttack);
            }else if (whichAttack > 2 && whichAttack <= 4){
                enemy.SwitchState(enemy.trackAttack);
            }else{
                enemy.SwitchState(enemy.normalAttack);
            }
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    
}