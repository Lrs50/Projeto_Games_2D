using UnityEngine;
using System.Collections;
public class EnemiesSearchState: BaseStateEnemies {

    private Vector3 startPosition;
    private Vector3 goToPosition;

    private bool shouldMove = false;
    
    public override void EnterState(EnemiesStateManager enemy){
        startPosition = enemy.transform.position;
        goToPosition = startPosition;

        Debug.Log(enemy.transform.position);
        Debug.Log(goToPosition);
        Debug.Log(Vector3.Distance(enemy.transform.position, goToPosition));
    }

    public override void UpdateState(EnemiesStateManager enemy){
        Debug.Log(enemy.transform.position);
        Debug.Log(goToPosition);
        Debug.Log(Vector3.Distance(enemy.transform.position, goToPosition));
        if (Vector3.Distance(enemy.transform.position, goToPosition) <= 2f) {
            if (enemy.waitTime <= 0){
                enemy.waitTime = enemy.startWaitTime;
                goToPosition = (Vector2) startPosition + Random.insideUnitCircle * 5f;
                shouldMove = true;
                Debug.Log("should move");
                Debug.Log(goToPosition);
            }
            else {
                enemy.waitTime -= Time.deltaTime;
                Debug.Log(enemy.waitTime);
            }
        }


        //fazer busca  
        if(Vector3.Distance(enemy.target.position, enemy.transform.position) <= enemy.maxRange && Vector3.Distance(enemy.target.position, enemy.transform.position) >= enemy.minRange){
            enemy.aggro += 150f * Time.deltaTime;
            if (enemy.aggro > 100){
                enemy.aggro = 100;
                foundPlayer(enemy);
                Debug.Log("aggro on enemy");
            }
        }
    }

    public override void FixedUpdateState(EnemiesStateManager enemy){
        if (shouldMove) {
            enemy.agent.isStopped = false;
            rotateTowardsTarget(enemy);
            enemy.agent.SetDestination(goToPosition);
        }
        else {
            enemy.agent.isStopped = true;
        }
    }

    public void rotateTowardsTarget(EnemiesStateManager enemy){
        var offset = 90f;
        Vector2 direction = goToPosition - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public override void OnCollisionEnter(EnemiesStateManager enemy){

    }

    public virtual void foundPlayer(EnemiesStateManager enemy){    
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position,enemy.baseSpeed *Time.deltaTime);    
        enemy.SwitchState(enemy.moveState);
    }
}
