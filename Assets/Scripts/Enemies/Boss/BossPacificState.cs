using UnityEngine;
using System.Collections;
public class BossPacificState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){

    }

    public override void UpdateState(BossStateManager enemy){
        if(Vector3.Distance(enemy.transform.position, enemy.target.transform.position) <= 5f){
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    private IEnumerator startBoss(BossStateManager enemy){ 
        yield return new WaitForSeconds(5f);
        enemy.readyToAttack = true;
    }
}