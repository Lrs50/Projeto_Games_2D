using UnityEngine;
using System.Collections;
public class BossDrillAttackState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.followingTime = 3;
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.followingTime <= 0){
            //enemy.agent.SetDestination(enemy.target.position);
            followPlayerX(enemy);
            Vector3 forward = Vector3.up * 10;
            if((enemy.target.transform.position.y - enemy.transform.position.y) <= 0){
                Debug.DrawRay(enemy.transform.position, -forward, Color.green);
                forward = -forward;
            }else{
                Debug.DrawRay(enemy.transform.position, forward, Color.green);
            }
            RaycastHit2D hit = Physics2D.Raycast((Vector2)enemy.transform.position,forward, Mathf.Infinity);
            if(hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player"){
                enemy.StartCoroutine(Dash(enemy));
            }
        }else{
            enemy.followingTime -= Time.deltaTime;
        }
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

    public void followPlayerX(BossStateManager enemy){
        Vector3 dest = new Vector3(enemy.target.transform.position.x,enemy.transform.position.y,enemy.target.transform.position.z);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,dest, enemy.baseSpeed * Time.deltaTime);
    }

    private IEnumerator Dash(BossStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        direction.Normalize();        
        enemy.rb.AddForce(direction * enemy.dashMag, ForceMode2D.Impulse);
        yield return new WaitForSeconds(enemy.dashTimer);      
        enemy.SwitchState(enemy.drillAttack);
    }
}