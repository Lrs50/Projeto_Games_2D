using UnityEngine;
using UnityEngine.UI;
using System;
public class BossFlyState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.rb.isKinematic = true;
        enemy.cd.enabled = false;
        enemy.spriteRenderer.sprite = enemy.shadowSprite;
        enemy.startFollowingTime = 0;
        enemy.agent.speed = enemy.flySpeed;
    }

    public override void UpdateState(BossStateManager enemy){
        enemy.startFollowingTime += Time.deltaTime;
        if(enemy.startFollowingTime < enemy.followingTime){
            enemy.agent.SetDestination(enemy.target.position);
            //followPlayer(enemy);
        }else{
            enemy.SwitchState(enemy.landingState);
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