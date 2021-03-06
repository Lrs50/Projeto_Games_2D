using UnityEngine;
using System.Collections;
public class BossGoingBackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        if(!enemy.audioSource.isPlaying){
            enemy.audioSource.clip = enemy.dashForteSound;
            enemy.audioSource.pitch = 5f;
            enemy.audioSource.Play();
        }
    }

    public override void UpdateState(BossStateManager enemy){
        if(Vector3.Distance(enemy.transform.position, enemy.goBack) > enemy.baseSpeed * Time.deltaTime){
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.goBack, 50f * Time.deltaTime);
        }else{
            //Debug.Log("1");
            enemy.rb.velocity = Vector2.zero;
            enemy.SwitchState(enemy.drillAttack);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
}