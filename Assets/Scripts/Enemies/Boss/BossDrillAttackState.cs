using UnityEngine;
using System.Collections;
public class BossDrillAttackState: BaseStateBoss {
    public bool dashed;
    bool prepare=true;
    bool attack = false;
    bool isDone = false;
    bool firstRot;
    bool firstPrepare;
    public override void EnterState(BossStateManager enemy){
        if(enemy.dashCounter == enemy.qtdDashDrillAttack){
            if(!isDone){
                enemy.wings_object.SetActive(true);
                enemy.StartCoroutine(EndOfAttack(enemy));
            }
        }
        dashed = false;
        firstRot = true;
        firstPrepare= true;
        enemy.followingTime = 3;
    }

    public override void UpdateState(BossStateManager enemy){
        if(enemy.followingTime <= 0 && !dashed){
            prepare = false;
            attack=true;
            followPlayerX(enemy);
            Vector3 forward = Vector3.up * 10;
            if((enemy.target.transform.position.y - enemy.transform.position.y) <= 0){
                //Debug.DrawRay(enemy.transform.position, -forward, Color.green);
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
                    //Debug.Log(enemy.goBack.x + "," +enemy.goBack.y + "," + enemy.goBack.z);
                    enemy.StartCoroutine(Dash(enemy,forward));
                }
            }        
        }else if(enemy.followingTime > 0){
            enemy.followingTime -= Time.deltaTime;
            if(!prepare && !isDone){
                enemy.spriteRenderer.sprite = enemy.attackAnimation[0];
                rotateTowardsPlayer(enemy);
            }
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){
        if(!isDone){
            if(prepare){
                //Debug.Log(enemy.index);
                if(!enemy.audioSource.isPlaying){
                    //loading2Sound(enemy);
                    enemy.audioSource.clip = enemy.loading2Sound;
                    enemy.audioSource.Play();
                }
                if(enemy.index >= enemy.transformAnimation.Length){
                    enemy.index=0;
                    prepare=false;
                    enemy.spriteRenderer.sprite = enemy.attackAnimation[0];
                }
                if(enemy.indexWings>=enemy.transformWingsAnimation.Length){
                    enemy.indexWings = 0;
                    if(!isDone && enemy.wings_object.activeSelf){
                        enemy.wings_object.SetActive(false);
                    }
                }
                enemy.wingsSR.sprite = enemy.transformWingsAnimation[enemy.indexWings];

                enemy.spriteRenderer.sprite = enemy.transformAnimation[enemy.index];
                if(firstPrepare){
                    rotateTowardsPlayer(enemy);
                    firstPrepare = false;
                }
            }else if(attack){
                if(enemy.index >= enemy.attackAnimation.Length){
                    enemy.index=0;
                }
                if(enemy.rb.velocity == Vector2.zero || firstRot){
                    rotateTowardsPlayer(enemy);
                    firstRot = false;
                }
                enemy.spriteRenderer.sprite = enemy.attackAnimation[enemy.index];
            }
        }
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
        enemy.audioSource.clip = enemy.dashForteSound;
        enemy.audioSource.pitch = 5f;
        enemy.audioSource.Play();  
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        //direction.Normalize();        
        enemy.rb.AddForce(direction * (enemy.dashMag*7), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        enemy.rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);  
        //enemy.rb.AddForce(forward * (enemy.dashMag + 2f), ForceMode2D.Impulse);   
        
        enemy.SwitchState(enemy.backState);
    }
    private IEnumerator EndOfAttack(BossStateManager enemy){
        if(!isDone){
            attack = false;
            isDone = true;
            if(!enemy.audioSource.isPlaying){
                enemy.audioSource.pitch = 1f;
                enemy.audioSource.clip = enemy.loadingSound;
                enemy.audioSource.Play();
            }
            enemy.transform.rotation = Quaternion.identity;
            for(int i=0;i<enemy.undoTransformAnimation.Length;i++){
                if(i<enemy.undoTransWingsformAnimation.Length) enemy.wingsSR.sprite = enemy.undoTransWingsformAnimation[i];
                enemy.spriteRenderer.sprite = enemy.undoTransformAnimation[i];
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.5f);
            isDone = false;    
            prepare=true;
            enemy.dashCounter = 0;
            enemy.SwitchState(enemy.searchState);
        }
        
    }

    private IEnumerator loading2Sound(BossStateManager enemy){
        enemy.audioSource.Play();
        yield return new WaitForSeconds(2f);
        enemy.audioSource.Stop();
    }
}