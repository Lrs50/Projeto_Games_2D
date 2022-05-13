using UnityEngine;
using System.Collections;
public class BossPacificState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.StartCoroutine(startBoss(enemy));
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.readyToAttack){
            enemy.SwitchState(enemy.searchState);
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    private IEnumerator startBoss(BossStateManager enemy){ 
        yield return new WaitForSeconds(2f);
        enemy.readyToAttack = true;
    }
}