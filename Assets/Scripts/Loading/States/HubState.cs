using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HubState : BaseStateScenes
{
    Scene scene;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.MainHub);
        scene= SceneManager.GetActiveScene();
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){
       if(GameManager.player!=null){
            if(GameManager.player.nextStage){
                gameManager.SwitchState(GameManager.p1_0);
            }
        }else if(scene.name=="MainHub"){
            GameManager.player=GameObject.Find("Player").GetComponent<PlayerStateManager>();
            gameManager.ResetPlayer();
        }else if(scene.name!="MainHub"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
