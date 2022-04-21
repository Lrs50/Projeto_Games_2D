using UnityEngine;

public abstract class BaseStateEnemies
{
   
   public abstract void EnterState(EnemiesStateManager enemy);

   public abstract void UpdateState(EnemiesStateManager enemy);

   public abstract void FixedUpdateState(EnemiesStateManager enemy);

   public abstract void OnCollisionEnter(EnemiesStateManager enemy);
   
}
