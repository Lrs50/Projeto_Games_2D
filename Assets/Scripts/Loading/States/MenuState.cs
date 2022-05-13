using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuState : BaseStateScenes
{
    Scene scene;
    bool playSound = true;
    public override void EnterState(GameManager gameManager){
        Loader.Load(Loader.Scene.MainMenu);
        scene= SceneManager.GetActiveScene();
        playSound = true;
    }

   public override void UpdateState(GameManager gameManager){
   }

   public override void FixedUpdateState(GameManager gameManager){
       if(scene.name=="MainMenu" && playSound){
            playSound=false;
            gameManager.audioSource.clip = gameManager.menuSound;
            gameManager.audioSource.Play();
       }else if(scene.name!="MainMenu"){
           scene= SceneManager.GetActiveScene();
       }
   }

   public override void OnCollisionEnter(GameManager gameManager){

   }
}
