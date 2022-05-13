using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HubState : BaseStateScenes
{
    Scene scene;
    bool playSound = true;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.MainHub);
        scene= SceneManager.GetActiveScene();
        playSound = true;
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
        if(scene.name=="MainHub" && playSound){
            playSound=false;
            gameManager.audioSource.clip = gameManager.fasesSound;
            gameManager.audioSource.volume=0.04f;
            gameManager.audioSource.Play();
        }else if(scene.name!="MainHub"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
