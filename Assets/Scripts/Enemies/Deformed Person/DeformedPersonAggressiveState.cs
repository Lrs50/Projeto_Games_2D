using UnityEngine;
using System.Collections;
public class DeformedPersonAggressiveState: BaseStateEnemies {
  public override void EnterState(EnemiesStateManager enemy) {
        enemy.agent.isStopped = false;
    }

    public override void UpdateState(EnemiesStateManager enemy) {
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) > enemy.maxRange){
            enemy.aggro -= 100f * Time.deltaTime;

            if (enemy.aggro < 0){
                enemy.aggro = 0;
                ExitState(enemy);
            }
            
            //Debug.Log(enemy.aggro); 
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        getAngle(enemy);
        enemy.agent.SetDestination(enemy.target.position);
        enemy.Animate();

    }

    public override void OnCollisionEnter(EnemiesStateManager enemy) {

    }

    public void ExitState(EnemiesStateManager enemy){
        //Debug.Log("Lost aggro on opponent");
        enemy.agent.isStopped = true;
        enemy.SwitchState(enemy.searchState);
    }

    public void getAngle(EnemiesStateManager enemy){

        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        
        Vector3 direction = toPosition - fromPosition;

        direction.Normalize();
        enemy.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        
    }

}