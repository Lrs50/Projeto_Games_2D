using UnityEngine;
using System.Collections;
public class BossDashAttackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.startDelayDashAttack = 0;
        enemy.rb.velocity = Vector2.zero;
    }

    public override void UpdateState(BossStateManager enemy){
        if(Input.GetKeyDown(KeyCode.G)){
            for (int i = 0; i < enemy.qtDash; i++)
            {
                enemy.StartCoroutine(Dash(enemy));                
            }
        }
        // enemy.startDelayDashAttack += Time.deltaTime;
        // if(enemy.startDelayDashAttack >= enemy.delayToDashAttack){
        //     enemy.StartCoroutine(Dash(enemy));        
        //     //enemy.SwitchState(enemy.searchState);
        // }else{
        //     //rotateTowardsPlayer(enemy);
        //     enemy.transform.position -= (enemy.target.position - enemy.transform.position).normalized * 5 * Time.deltaTime; 
        // }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    private IEnumerator Dash(BossStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        direction.Normalize();        
        enemy.rb.AddForce(direction * enemy.dashMag, ForceMode2D.Impulse);
        yield return new WaitForSeconds(enemy.dashTimer);      
        enemy.SwitchState(enemy.searchState);
    }
}
