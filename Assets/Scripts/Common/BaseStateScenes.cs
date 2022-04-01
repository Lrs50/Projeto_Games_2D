using UnityEngine;

public abstract class BaseStateScenes
{
   
   public abstract void EnterState(GameManager gameManager);

   public abstract void UpdateState(GameManager gameManager);

   public abstract void FixedUpdateState(GameManager gameManager);

   public abstract void OnCollisionEnter(GameManager gameManager);
}
