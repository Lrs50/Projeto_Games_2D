using UnityEngine;

public abstract class BaseStateBoss
{
   
   public abstract void EnterState(BossStateManager enemy);

   public abstract void UpdateState(BossStateManager enemy);

   public abstract void FixedUpdateState(BossStateManager enemy);

   public abstract void OnCollisionEnter(BossStateManager enemy);

   public void rotateTowardsPlayer(BossStateManager enemy){
        var offset = 90f;
        Vector2 direction = enemy.target.position - enemy.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        enemy.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
