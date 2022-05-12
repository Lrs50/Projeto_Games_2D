using UnityEngine;
using System.Collections;
public class BossPacificState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
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
}