using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class P1_1State : BaseStateScenes
{
    Scene scene;
    bool playSound = true;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Phase1_1);
        scene= SceneManager.GetActiveScene();
        playSound = true;
    }

   public override void UpdateState(GameManager gameManager){

   }

   public override void FixedUpdateState(GameManager gameManager){
        if(GameManager.player!=null){
            if(GameManager.player.nextStage){
                gameManager.SwitchState(GameManager.p1_2);
            }
        }else if(scene.name=="Phase1_1"){
            PlayerStateManager old = GameManager.player;
            GameManager.player=GameObject.Find("Player").GetComponent<PlayerStateManager>();
            if(GameManager.player!=null){
                gameManager.UpdatePlayer(old);
            };
        }else if(scene.name!="Phase1_1"){
            scene= SceneManager.GetActiveScene();
        }
        if(scene.name=="Phase1_1" && playSound){
            playSound=false;
            gameManager.audioSource.clip = gameManager.combateSound;
            gameManager.audioSource.Play();
        }else if(scene.name!="Phase1_1"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
