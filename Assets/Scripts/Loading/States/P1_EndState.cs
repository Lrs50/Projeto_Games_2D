using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1_EndState  : BaseStateScenes
{
    Scene scene;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.PhaseFinal);
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){
       if(GameManager.player!=null){
            if(GameManager.player.nextStage){
                gameManager.SwitchState(GameManager.credits);
            }
        }else if(scene.name=="PhaseFinal"){
            PlayerStateManager old = GameManager.player;
            GameManager.player=GameObject.Find("Player").GetComponent<PlayerStateManager>();
            if(GameManager.player!=null){
                gameManager.UpdatePlayer(old);
            };
        }else if(scene.name!="PhaseFinal"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
