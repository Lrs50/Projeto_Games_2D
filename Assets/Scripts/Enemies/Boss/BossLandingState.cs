using UnityEngine;
using System.Collections;
public class BossLandingState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.rb.isKinematic = false;
        enemy.cd.enabled = true;
        enemy.spriteRenderer.sprite = enemy.bossSprite;
        enemy.StartCoroutine(aoeDamage(enemy));
    }

    public override void UpdateState(BossStateManager enemy){
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    private IEnumerator aoeDamage(BossStateManager enemy){
        enemy.AreaDamage(enemy.transform.position,5);
        yield return new WaitForSeconds(0.1f);
        enemy.SwitchState(enemy.searchState);
    } 
}