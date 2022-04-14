using UnityEngine;
using System.Collections;
public class DeformedSearchState: EnemiesSearchState {
    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        //fazer busca  
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange){
            rotateTowardsPlayer(enemy);
            foundPlayer(enemy);            
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){

    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public override void foundPlayer(EnemiesStateManager enemy){    
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);    
        enemy.SwitchState(enemy.attackState);
    }

    public void rotateTowardsPlayer(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
