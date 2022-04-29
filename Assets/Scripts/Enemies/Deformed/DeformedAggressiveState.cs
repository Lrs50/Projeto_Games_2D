using UnityEngine;
using System.Collections;
public class DeformedAggressiveState : BaseStateEnemies {
    public bool shoot = false;
    public bool canShoot = false;
    float shootDelay = 200;
    public float shootAnimationTime = 40;
    public int count = 0;
    Vector3 direction;

    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange) {
            isShootable(enemy);        
        }
        else{
            enemy.SwitchState(enemy.searchState);
        }

    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        enemy.Animate();
        
        count++;
        if(count>=shootDelay && shoot==false){
            if(canShoot){
                shoot=true;
                enemy.animationState="attack";
            }
            count=0;
        }

        if(shoot==true && Mathf.Floor(shootAnimationTime/2)==count){
            enemy.OnShoot(direction);
        }

        if(count>=shootAnimationTime && shoot ==true){
            shoot = false;
            count=0;
            enemy.animationState="idle";
        }

        
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public void isShootable(EnemiesStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        // linha para atirar
        direction = toPosition - fromPosition;

        direction.Normalize();
        enemy.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, Mathf.Infinity);
        if(hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player"){
            canShoot = true;
        }else{
            canShoot = false;
        }
    }

}
