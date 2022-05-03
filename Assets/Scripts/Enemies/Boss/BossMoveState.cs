using UnityEngine;
using System.Collections;
public class BossMoveState : BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
    }

    public override void UpdateState(BossStateManager enemy){
        //enemy.transform.LookAt(enemy.target.transform);
        rotateTowardsPlayer(enemy);
        followPlayer(enemy);
        if(Vector3.Distance(enemy.target.position, enemy.transform.position)<= enemy.maxRange){
            enemy.SwitchState(enemy.dashAttack);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    public void followPlayer(BossStateManager enemy){
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);
    }
}