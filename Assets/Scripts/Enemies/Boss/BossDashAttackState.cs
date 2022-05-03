using UnityEngine;
using System.Collections;
public class BossDashAttackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.startDelayDashAttack = 0;
        enemy.rb.velocity = Vector2.zero;
    }

    public override void UpdateState(BossStateManager enemy){
        //enemy.transform.LookAt(enemy.target.transform);
        enemy.startDelayDashAttack += Time.deltaTime;
        if(enemy.startDelayDashAttack >= enemy.delayToDashAttack){        
            enemy.StartCoroutine(Dash(enemy));
        }else{
            rotateTowardsPlayer(enemy);
            enemy.transform.position -= (enemy.target.position - enemy.transform.position).normalized * 5 * Time.deltaTime; 
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){

    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    private IEnumerator Dash(BossStateManager enemy){
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction = direction.normalized;
        enemy.rb.AddForce(direction*enemy.dashMag,ForceMode2D.Impulse);
        enemy.transform.rotation = Quaternion.LookRotation(Vector3.forward,direction);
        yield return new WaitForSeconds(enemy.dashTimer);
        enemy.SwitchState(enemy.searchState);
    } 
}
