using UnityEngine;
using System.Collections;
public class DeformedAggressiveState : BaseStateEnemies {
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
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public void isShootable(EnemiesStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        // linha para atirar
        Vector3 direction = toPosition - fromPosition;

        direction.Normalize();
        enemy.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, Mathf.Infinity);
        if(hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player"){
            //Debug.Log("Shootable! Pew pew");
        }else{
            //Debug.Log(hit);
            //Debug.Log("Encontrei o Jogador, mas tem um obstaculo na frente!");
        }
    }

}
