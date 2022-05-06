using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossSearchState: BaseStateBoss {
    public override void EnterState(BossStateManager enemy){
        enemy.rb.velocity = Vector2.zero;
        enemy.timerForAttacks = 0;
    }

    public override void UpdateState(BossStateManager enemy){
        //rotateTowardsPlayer(enemy);
        float dist = Vector3.Distance(enemy.transform.position,enemy.target.transform.position);
        if(dist > 5f){
            followPlayer(enemy);
            //enemy.agent.SetDestination(enemy.target.position);
        }else if(dist < 4.95f){
            Vector3 flee = (enemy.target.transform.position - enemy.transform.position).normalized;
            followPoint(enemy,flee);
            //enemy.agent.SetDestination((enemy.target.transform.position - enemy.transform.position).normalized * 5);
        }
        enemy.timerForAttacks += Time.deltaTime;
        if(enemy.timerForAttacks > enemy.randomAttackInterval){
            //int whichAttack = 6;//Random.Range(1,4);
            //Debug.Log(whichAttack);
            if(enemy.maxHealth*0.9 <= enemy.currHealth && enemy.currHealth <= enemy.maxHealth){
                enemy.SwitchState(enemy.normalAttack);
            }else if(enemy.maxHealth*0.7 <= enemy.currHealth && enemy.currHealth <= enemy.maxHealth *0.9){
                int whichAttack = Random.Range(1,3);
                if( whichAttack == 1){
                    enemy.SwitchState(enemy.normalAttack);
                }else{
                    enemy.SwitchState(enemy.trackAttack);
                }
            }else if(enemy.maxHealth*0.5 <= enemy.currHealth && enemy.currHealth < enemy.maxHealth *0.7){
                float whichAttack = Random.Range(0f,1f);
                if(whichAttack<=0.40f){
                    enemy.SwitchState(enemy.trackAttack);
                }else if(0.40f < whichAttack && whichAttack <= 0.70f){
                    enemy.SwitchState(enemy.normalAttack);
                }else if(0.70f < whichAttack && whichAttack <= 0.90f){
                    enemy.SwitchState(enemy.dashAttack);
                }else{
                    enemy.SwitchState(enemy.flyingState);
                }
            }else if (enemy.maxHealth*0.4 <= enemy.currHealth && enemy.currHealth < enemy.maxHealth *0.5){
                float whichAttack = Random.Range(0f,1f);
                if(whichAttack<=0.50f){
                    enemy.SwitchState(enemy.attackState);
                }else if(0.50f < whichAttack && whichAttack <= 0.85f){
                    enemy.SwitchState(enemy.dashAttack);
                }else{
                    enemy.SwitchState(enemy.flyingState);
                }
            }else if (enemy.maxHealth*0.2 <= enemy.currHealth && enemy.currHealth < enemy.maxHealth *0.4){
                float whichAttack = Random.Range(0f,1f);
                if(whichAttack<=0.20f){
                    enemy.SwitchState(enemy.trackAttack);
                }else if(0.20f < whichAttack && whichAttack <= 0.40f){
                    enemy.SwitchState(enemy.attackState);
                }else if(0.40f < whichAttack && whichAttack <= 0.60f){
                    enemy.SwitchState(enemy.flyingState);
                }else if(0.60f < whichAttack && whichAttack <= 0.80f){
                    enemy.SwitchState(enemy.dashAttack);//um 
                }else{
                    enemy.SwitchState(enemy.dashAttack);//random 2 e 5
                }
            }else{
                //boss mais rápido
                float whichAttack = Random.Range(1,4);
                if(whichAttack == 1){
                    enemy.SwitchState(enemy.attackState);
                }else if(whichAttack == 2){
                    enemy.SwitchState(enemy.dashAttack);//varios dashes
                }else{
                    enemy.SwitchState(enemy.drillAttack);
                }
            }
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){
        if(enemy.index>=enemy.idleAnimation.Length){
            enemy.index = 0;
        }
        if(enemy.indexWings>=enemy.idleWingsAnimation.Length){
            enemy.indexWings = 0;
        }
        enemy.wingsSR.sprite = enemy.idleWingsAnimation[enemy.index];
        enemy.spriteRenderer.sprite = enemy.idleAnimation[enemy.index];



    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    public void followPlayer(BossStateManager enemy){
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);
    }
    public void followPoint(BossStateManager enemy,Vector3 flee){
        enemy.transform.position = Vector3.Lerp(enemy.transform.position, enemy.transform.position - flee, enemy.baseSpeed * Time.deltaTime);
        //enemy.transform.position -= flee * (enemy.baseSpeed +speed) * Time.deltaTime;
    }
}