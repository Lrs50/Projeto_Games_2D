using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1_2State  : BaseStateScenes
{
    Scene scene;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Phase1_2);
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){
        if(GameManager.player!=null){
            if(GameManager.player.nextStage){
                gameManager.SwitchState(GameManager.p1_Final);
            }
        }else if(scene.name=="Phase1_2"){
            PlayerStateManager old = GameManager.player;
            GameManager.player=GameObject.Find("Player").GetComponent<PlayerStateManager>();
            if(GameManager.player!=null){
                gameManager.UpdatePlayer(old);
            };
        }else if(scene.name!="Phase1_2"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
