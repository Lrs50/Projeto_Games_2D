using UnityEngine;
using System.Collections;
public class BossDrillAttackState: BaseStateBoss {
    public bool dashed;
    public override void EnterState(BossStateManager enemy){
        if(enemy.dashCounter == enemy.qtdDashDrillAttack){
            enemy.dashCounter = 0;
            enemy.SwitchState(enemy.searchState);
        }
        dashed = false;
        enemy.followingTime = 3;
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.followingTime <= 0){
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
                if(!dashed){
                    dashed = true;
                    enemy.dashCounter++;
                    enemy.goBack = enemy.transform.position;
                    Debug.Log(enemy.goBack.x + "," +enemy.goBack.y + "," + enemy.goBack.z);
                    enemy.StartCoroutine(Dash(enemy,forward));
                }
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

    private IEnumerator Dash(BossStateManager enemy, Vector3 forward){    
        enemy.rb.AddForce(forward * (enemy.dashMag + 2f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(enemy.dashTimer);      
        enemy.SwitchState(enemy.backState);
    }
}