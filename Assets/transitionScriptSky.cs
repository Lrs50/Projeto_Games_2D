using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionScriptSky : MonoBehaviour
{
    public Animator animator;
    public openTrigger aux;
    // Update is called once per frame
    void Update()
    {
        if(aux.toSky){
            fadeOut();
        }
    }

    public void fadeOut(){
        animator.SetTrigger("fadeOut");
    }

    public void fadeIn(){
        animator.SetTrigger("fadeIn");
    }

    public void onCompletefadeOut(){
        Vector3 temp = new Vector3(aux.cam.transform.position.x, aux.cam.transform.position.y);
        aux.nextObj.transform.position = temp;
        aux.verde.SetActive(false);
        aux.camScript.velH = 0;
        aux.camScript.velV = 0.3f;
        //camScript.velV = -0.1f;
        aux.nextObj.SetActive(true);
        aux.dManager.bloquearDialogo = false;
        aux.dManager.gameObject.transform.parent.transform.localScale = new Vector3(10,10,1);
        aux.dManager.counter = 0;
        fadeIn();
    }
}
