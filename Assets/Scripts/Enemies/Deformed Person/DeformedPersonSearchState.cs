using UnityEngine;
using System.Collections;
public class DeformedPersonSearchState: EnemiesSearchState {
    public override void EnterState(EnemiesStateManager enemy){
    }

    public override void UpdateState(EnemiesStateManager enemy){
        //fazer busca  
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange){
            foundPlayer(enemy);            
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){

    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    // public void foundPlayer(EnemiesStateManager enemy){    
    //     //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);    
    //     enemy.SwitchState(enemy.moveState);
    // }
}
