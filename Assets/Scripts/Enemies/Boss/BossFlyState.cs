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
        enemy.shadow.transform.localScale = new Vector3(2f,2f,1f);
        enemy.startFollowingTime = 0;
        
    }

    public override void UpdateState(BossStateManager enemy){
        float dist = Vector3.Distance(enemy.shadow.transform.position,enemy.transform.position);
        //enemy.shadow.transform.position = new Vector3(enemy.transform.position.x, enemy.shadow.transform.position.y + (enemy.transform.position.y - enemy.shadow.transform.position.y), enemy.transform.position.z);
        if(dist < 10){
            //enemy.shadow.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.transform.position + new Vector3(0,10,0), enemy.baseSpeed * Time.deltaTime);
            enemy.shadow.transform.position += Vector3.up *(enemy.flySpeed)*Time.deltaTime;
        }else{
            enemy.startFollowingTime += Time.deltaTime;
            if(enemy.startFollowingTime < enemy.followingTime){
                //enemy.agent.SetDestination(enemy.target.position);
                followPlayer(enemy);
            }else{
                enemy.SwitchState(enemy.landingState);
            }

        }

        
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    public void followPlayer(BossStateManager enemy){
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.flySpeed *Time.deltaTime);
    }

}