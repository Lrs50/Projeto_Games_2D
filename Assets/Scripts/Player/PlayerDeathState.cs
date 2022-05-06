using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDeathState : BaseStatePlayer{
    int count = 0;
    int index = 0;
    int animationTimer = 1;
    bool userInput = false;
    int deathMode =0;
    Sprite[] death;
    public override void EnterState(PlayerStateManager player) {
        deathMode= Random.Range(0,3);
        Time.timeScale=0.05f;

        if(deathMode==0){
            death = player.death1;
        }else if(deathMode==1){
            death = player.death2;
        }else{
            death = player.death3;
        }
        
    }

    public override void UpdateState(PlayerStateManager player) {

        if(userInput){
            if(Input.GetKeyDown(KeyCode.Return)){
                Time.timeScale=1f;
                Loader.Load(Loader.Scene.MainHub);
            }
        }
    }

    public override void FixedUpdateState(PlayerStateManager player) {
        count++;
        if(userInput){
            SpriteRenderer background = player.deathUI.transform.GetChild(1).GetComponent<SpriteRenderer>();
            if(background.color.a <= 1){
                if(background.color.a+0.05f>1){
                    background.color = new Color(0,0,0,1);
                }else{
                    background.color = new Color(0,0,0,background.color.a+0.05f);
                }
            }
            
        }

        if(count>=animationTimer){
            count=0;
            index++;
        }
        if(index>=death.Length){
            index=death.Length-1;
            player.deathUI.SetActive(true);
            userInput = true;
        }

        player.spriteRenderer.sprite = death[index];
    }
    

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

}
