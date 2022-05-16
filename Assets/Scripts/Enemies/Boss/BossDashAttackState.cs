using UnityEngine;
using System.Collections;
public class BossDashAttackState: BaseStateBoss {
    public bool dashed;
    bool prepare = true;
    bool attack = false;
    bool normal = false;
    bool firstRot = true;
    Quaternion originalRotation;
    public override void EnterState(BossStateManager enemy){
        prepare = true;
        attack = false;
        normal = false;
        firstRot = true;
        enemy.startDelayDashAttack = 0;
        enemy.rb.velocity = Vector2.zero;
        dashed = false;
        originalRotation = enemy.transform.rotation;
        enemy.index=0;

    }

    public override void UpdateState(BossStateManager enemy){
        
        enemy.startDelayDashAttack += Time.deltaTime;
        if(enemy.startDelayDashAttack >= enemy.delayToDashAttack){
            if(!normal){
                attack=true;
            }
            if(!dashed){
                dashed = true;
                enemy.StartCoroutine(Dash(enemy));
            }
            //enemy.StartCoroutine(Dash(enemy));        
            //enemy.SwitchState(enemy.searchState);
        }else{
            
            // enemy.transform.position -= (enemy.target.position - enemy.transform.position).normalized * 5 * Time.deltaTime; 
        }
    }

    public override void FixedUpdateState(BossStateManager enemy){
        if(prepare){
            //Debug.Log(enemy.index);
            if(!enemy.audioSource.isPlaying){
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
                enemy.wings_object.SetActive(false);
            }
            enemy.wingsSR.sprite = enemy.transformWingsAnimation[enemy.indexWings];

            enemy.spriteRenderer.sprite = enemy.transformAnimation[enemy.index];
            

        }else if(attack){
            if(enemy.index >= enemy.attackAnimation.Length){
                enemy.index=0;
            }
            if(enemy.rb.velocity == Vector2.zero || firstRot){
                rotateTowardsPlayer(enemy);
                firstRot = false;
            }
            enemy.spriteRenderer.sprite = enemy.attackAnimation[enemy.index];
        }else if(normal){
            if(enemy.indexWings>=enemy.undoTransWingsformAnimation.Length){
                enemy.indexWings = 0;
                enemy.wings_object.SetActive(true);
            }
            if(!enemy.audioSource.isPlaying){
                enemy.audioSource.pitch = 1f;
                enemy.audioSource.clip = enemy.loadingSound;
                enemy.audioSource.Play();
            }
            enemy.wingsSR.sprite = enemy.undoTransWingsformAnimation[enemy.indexWings];

            if(enemy.index >= enemy.undoTransformAnimation.Length){
                enemy.index=0;
                //enemy.audioSource.pitch = 1f;
                enemy.SwitchState(enemy.searchState);
            }
            enemy.spriteRenderer.sprite = enemy.undoTransformAnimation[enemy.index];
        }
    }

    public override void OnCollisionEnter(BossStateManager enemy){

    }
    private IEnumerator Dash(BossStateManager enemy){        
        //for (int i = 0; i < enemy.qtdDash; i++)
        for (int i = 0; i < Random.Range(2,6); i++)
        {
            enemy.audioSource.clip = enemy.dashForteSound;
            enemy.audioSource.pitch = 5f;
            enemy.audioSource.Play();
            Vector3 fromPosition = enemy.transform.position;
            Vector3 toPosition = enemy.target.transform.position;
            Vector3 direction = toPosition - fromPosition;
            direction.Normalize();
            enemy.rb.AddForce(direction * (enemy.dashMag * 40), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            enemy.rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            
        }
        attack=false;
        normal=true;
        enemy.transform.rotation=originalRotation;
    }
}
