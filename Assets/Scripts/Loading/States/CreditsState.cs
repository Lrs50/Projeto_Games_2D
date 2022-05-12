using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsStates : BaseStateScenes
{
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Credits);
    }

   public override void UpdateState(GameManager gameManager){
        if(Input.GetKeyDown(KeyCode.Escape)){
            gameManager.SwitchState(GameManager.menuState);
        }
   }

   public override void FixedUpdateState(GameManager gameManager){

   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
