using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreStartState : BaseStateScenes
{
    bool exit = false;
    Scene scene;
    bool playSound = true;

    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Pre_start);
        scene= SceneManager.GetActiveScene();
        playSound = true;
    }

   public override void UpdateState(GameManager gameManager){
   }

   public override void FixedUpdateState(GameManager gameManager){
        if(exit){
            gameManager.SwitchState(GameManager.hubState);
        }
        if(scene.name=="Pre_start" && playSound){
            playSound=false;
            gameManager.audioSource.clip = gameManager.fasesSound;
            gameManager.audioSource.volume=0.04f;
            gameManager.audioSource.Play();
        }else if(scene.name!="Pre_start"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }

    public void Exit(){
       exit=true;
   }
}
