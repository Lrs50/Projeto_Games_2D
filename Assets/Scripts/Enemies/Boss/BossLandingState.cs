using UnityEngine;
using System.Collections;
public class BossLandingState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.shadow.gameObject.SetActive(false);
        enemy.shadow.spriteRenderer.sprite = enemy.shadowSprite;
        enemy.spriteRenderer.sprite = enemy.idleAnimation[0];
        enemy.rb.isKinematic = false;
        enemy.cd.enabled = true;
        enemy.transform.localScale = new Vector3(6f,6f,6f);
        enemy.shadow.transform.localScale = new Vector3(1f,1f,1f);
        enemy.shadow.transform.position = enemy.transform.position;
        //enemy.spriteRenderer.sprite = enemy.bossSprite;
        //rotateTowardsPlayer(enemy);
        enemy.StartCoroutine(aoeDamage(enemy));
    }

    public override void UpdateState(BossStateManager enemy){        
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }

    private IEnumerator aoeDamage(BossStateManager enemy){
        enemy.AreaDamage(enemy.transform.position,2);
        enemy.wings_object.transform.localScale = Vector3.one;
        Vector3 aux = new Vector3(enemy.transform.position.x,enemy.transform.position.y + 0.085f*6,enemy.transform.position.z);
        enemy.wings_object.transform.position = aux;
        //enemy.wings_object.transform.position = new Vector3(0,0.085f*6,0);
        yield return new WaitForSeconds(0.5f);
        enemy.SwitchState(enemy.searchState);
    } 
}