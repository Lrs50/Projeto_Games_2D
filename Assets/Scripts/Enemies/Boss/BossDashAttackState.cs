using UnityEngine;
using System.Collections;
public class BossDashAttackState: BaseStateBoss {
    public bool dashed;
    public override void EnterState(BossStateManager enemy){
        enemy.startDelayDashAttack = 0;
        enemy.rb.velocity = Vector2.zero;
        dashed = false;
    }

    public override void UpdateState(BossStateManager enemy){
        
        enemy.startDelayDashAttack += Time.deltaTime;
        if(enemy.startDelayDashAttack >= enemy.delayToDashAttack){
            if(!dashed){
                dashed = true;
                enemy.StartCoroutine(Dash(enemy));
            }
            //enemy.StartCoroutine(Dash(enemy));        
            //enemy.SwitchState(enemy.searchState);
        }else{
            //rotateTowardsPlayer(enemy);
            enemy.transform.position -= (enemy.target.position - enemy.transform.position).normalized * 5 * Time.deltaTime; 
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    private IEnumerator Dash(BossStateManager enemy){
        for (int i = 0; i < enemy.qtdDash; i++)
        {
            Vector3 fromPosition = enemy.transform.position;
            Vector3 toPosition = enemy.target.transform.position;
            Vector3 direction = toPosition - fromPosition;
            //direction.Normalize();        
            enemy.rb.AddForce(direction * (enemy.dashMag+2f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            enemy.rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            
        }
        enemy.SwitchState(enemy.searchState);
    }
}
