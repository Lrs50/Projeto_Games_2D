using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreStartState : BaseStateScenes
{
    bool exit = false;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Pre_start);
    }

   public override void UpdateState(GameManager gameManager){
   }

   public override void FixedUpdateState(GameManager gameManager){
       if(exit){
           gameManager.SwitchState(GameManager.hubState);
       }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }

    public void Exit(){
       exit=true;
   }
}
