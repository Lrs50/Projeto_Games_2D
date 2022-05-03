using UnityEngine;
using UnityEngine.UI;
using System;
public class BossFlyState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.shadow.transform.position = enemy.transform.position;
        enemy.shadow.gameObject.SetActive(true);
        enemy.rb.isKinematic = true;
        enemy.cd.enabled = false;
        Vector3 rotateVector = new Vector3(0, enemy.shadow.transform.eulerAngles.y, 0);
        enemy.shadow.transform.rotation = Quaternion.Euler(rotateVector);
        enemy.shadow.spriteRenderer.sprite = enemy.bossSprite;
        enemy.spriteRenderer.sprite = enemy.shadowSprite;
        enemy.transform.localScale = new Vector3(0.5f,0.5f,1f);
        enemy.shadow.transform.localScale = new Vector3(1f,1f,1f);
        enemy.startFollowingTime = 0;
        enemy.agent.speed = enemy.flySpeed;
        
    }

    public override void UpdateState(BossStateManager enemy){
        enemy.shadow.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 10f, enemy.transform.position.z);
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