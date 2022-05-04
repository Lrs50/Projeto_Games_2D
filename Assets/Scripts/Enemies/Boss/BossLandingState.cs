using UnityEngine;
using System.Collections;
public class BossLandingState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.shadow.gameObject.SetActive(false);
        enemy.shadow.spriteRenderer.sprite = enemy.shadowSprite;
        enemy.spriteRenderer.sprite = enemy.bossSprite;
        enemy.rb.isKinematic = false;
        enemy.cd.enabled = true;
        enemy.transform.localScale = new Vector3(1f,1f,1f);
        enemy.shadow.transform.localScale = new Vector3(0.5f,0.5f,1f);
        enemy.shadow.transform.position = enemy.transform.position;
        //enemy.spriteRenderer.sprite = enemy.bossSprite;
        rotateTowardsPlayer(enemy);
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
        yield return new WaitForSeconds(0.5f);
        enemy.SwitchState(enemy.searchState);
    } 
}