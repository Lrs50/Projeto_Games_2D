using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovUpFolhas : MonoBehaviour
{
    public Animator animator;
    public float vel;
    public GameObject cam;
    public GameObject bg;
    public GameObject folhas;
    public CameraMov camScript;
    public DialogueManager dManager;
    public bool canMove;
    public GameObject fadeCanvas;
    public bool finalCond;


    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        camScript = cam.GetComponent<CameraMov>();
        canMove =false;
        finalCond = false;
    }
    void Update()
    {
        if(cam.transform.position.y > 3.4f){
            finalCond = true;
            //camScript.velV = 0;
            folhas.transform.position = new Vector3(cam.transform.position.x + 0.29f, cam.transform.position.y+1.7f-3.4f);
            bg.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y);
        }

        if(dManager && dManager.counter >= 3){
            canMove = true;
            dManager.bloquearDialogo = true;
            dManager.gameObject.transform.parent.transform.localScale = new Vector3(0,0,0);
        }
        if(canMove && finalCond){
            fadeCanvas.SetActive(true);
        }
    }

    
}
