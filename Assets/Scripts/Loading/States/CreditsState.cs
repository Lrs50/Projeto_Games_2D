using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsStates : BaseStateScenes
{
    Scene scene;
    bool playSound = true;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.Credits);
        scene= SceneManager.GetActiveScene();
        playSound = true;
    }

   public override void UpdateState(GameManager gameManager){
        if(Input.GetKeyDown(KeyCode.Escape)){
            gameManager.SwitchState(GameManager.menuState);
        }
   }

   public override void FixedUpdateState(GameManager gameManager){
        if(scene.name=="Credits" && playSound ){
            playSound=false;
            gameManager.audioSource.clip = gameManager.bonusSound;
            gameManager.audioSource.Play();
        }else if(scene.name!="Credits"){
            scene= SceneManager.GetActiveScene();
        }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
