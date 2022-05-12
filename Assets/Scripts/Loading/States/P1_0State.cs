using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class P1_0State : BaseStateScenes
{
    Scene scene;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Phase1_0);
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){
        if(GameManager.player!=null){
            if(GameManager.player.nextStage){
                gameManager.SwitchState(GameManager.p1_1);
            }
        }else if(scene.name=="Phase1_0"){
            PlayerStateManager old = GameManager.player;
            GameManager.player=GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
            gameManager.UpdatePlayer(old);
        }else if(scene.name!="Phase1_0"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
