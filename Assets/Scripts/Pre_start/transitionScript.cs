using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionScript : MonoBehaviour
{
    public Animator animator;
    public GameObject parallax;
    public GameObject nextObj;
    public GameObject cam;
    private CameraMov camScript;
    private bool verdeMov;
    private DialogueManager dManager;
    public bool triggered;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        camScript = cam.GetComponent<CameraMov>();
        dManager = FindObjectOfType<DialogueManager>();
        // verdeMov = false;
        // triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dManager.counter >= 2){
            //Change
            //Debug.Log("once");
            fadeOut();
            dManager.bloquearDialogo = true;
            dManager.gameObject.transform.parent.transform.localScale = new Vector3(0,0,0);            
        }
        // if(Input.GetMouseButtonDown(0)){
        //     fadeOut();
        // }else if(Input.GetMouseButtonDown(1)){
        //     fadeIn();
        // }
        // if(verdeMov && cam.transform.position.y > nextObj.transform.position.y){
        //     nextObj.transform.position = new Vector3(nextObj.transform.position.x,nextObj.transform.position.y+(0.1f*Time.deltaTime),nextObj.transform.position.z);
        // }else if(verdeMov){
        //     triggered = true;
        //     verdeMov = false;
        // }
    }

    public void fadeOut(){
        animator.SetTrigger("fadeOut");
    }

    public void fadeIn(){
        animator.SetTrigger("fadeIn");
    }

    public void onFadeCompleteFirst(){
        verdeMov = true;
        Vector3 temp = new Vector3(cam.transform.position.x, cam.transform.position.y-1.57f);
        nextObj.transform.position = temp;
        parallax.SetActive(false);
        camScript.velH = 0;
        camScript.velV = 0;
        //camScript.velV = -0.1f;
        nextObj.SetActive(true);
        gameObject.SetActive(false);
        fadeIn();
        dManager.bloquearDialogo = false;
        dManager.gameObject.transform.parent.transform.localScale = new Vector3(10,10,1);
        dManager.counter = 0;
    }
}
