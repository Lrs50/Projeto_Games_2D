using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseStateScenes
{
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.MainMenu);
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){

   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
