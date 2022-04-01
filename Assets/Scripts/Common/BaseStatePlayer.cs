using UnityEngine;

public abstract class BaseStatePlayer
{
   
   public abstract void EnterState(PlayerStateManager player);

   public abstract void UpdateState(PlayerStateManager player);

   public abstract void FixedUpdateState(PlayerStateManager player);

   public abstract void OnCollisionEnter(PlayerStateManager player);
}
