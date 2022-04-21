using UnityEngine;
using System.Collections;
public class DeformedAggressiveState : BaseStateEnemies {
    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange){
            rotateTowardsPlayer(enemy);
            isShootable(enemy);        
        }else{
            enemy.SwitchState(enemy.searchState);
        }

    }

    public override void FixedUpdateState(EnemiesStateManager enemy){

    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public void rotateTowardsPlayer(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void isShootable(EnemiesStateManager enemy){
        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        Vector3 direction = toPosition - fromPosition;
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction,Mathf.Infinity , enemy.obstacles);
        //Debug.Log(hit.collider.gameObject.name);
        if(hit.collider != null && hit.collider.tag == "Player"){
            Debug.Log("Shootable! Pew pew");
        }else{
            Debug.Log("Encontrei o Jogador, mas tem um obstaculo na frente!");
        }
    }

}
