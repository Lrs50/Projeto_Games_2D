using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1_EndState  : BaseStateScenes
{
    Scene scene;
    bool playSound = true;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.PhaseFinal);
        scene= SceneManager.GetActiveScene();
        playSound = true;
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
                GameManager.player.setWings(true);
            };
        }else if(scene.name!="PhaseFinal"){
            scene= SceneManager.GetActiveScene();
        }
        if(scene.name=="PhaseFinal" && playSound){
            playSound=false;
            gameManager.audioSource.clip = gameManager.bonusSound;
            gameManager.audioSource.Play();
        }else if(scene.name!="PhaseFinal"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
