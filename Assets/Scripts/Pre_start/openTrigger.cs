using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openTrigger : MonoBehaviour
{
    public Animator animator;
    public GameObject verde;
    [SerializeField] public GameObject cam;
    public CameraMov camScript;
   // [SerializeField] private GameObject image;
    [SerializeField] public GameObject nextObj;
    public VerdeMov checking;
    public DialogueManager dManager;
    public bool canMove;
    public bool toSky;
    public GameObject fadeCanvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        verde = transform.parent.gameObject;
        checking = verde.GetComponent<VerdeMov>();
        camScript = cam.GetComponent<CameraMov>();
        dManager = FindObjectOfType<DialogueManager>();
        canMove =false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(checking.triggered);
        if(dManager && dManager.counter >= 1){
            canMove = true;
            dManager.bloquearDialogo = true;
            dManager.gameObject.transform.parent.transform.localScale = new Vector3(0,0,0);
        }
        if(checking.triggered && canMove){
            openEyes();
            checking.triggered = false;            
            //dManager.counter = 0;
        }
    }
    public void openEyes(){
        animator.SetTrigger("opening");
    }

    public void onCompleteFirst(){
        toSky = true;
        fadeCanvas.SetActive(true);
        // Vector3 temp = new Vector3(cam.transform.position.x, cam.transform.position.y);
        // nextObj.transform.position = temp;
        // verde.SetActive(false);
        // camScript.velH = 0;
        // camScript.velV = 0.3f;
        // //camScript.velV = -0.1f;
        // nextObj.SetActive(true);
        // dManager.bloquearDialogo = false;
        // dManager.gameObject.transform.parent.transform.localScale = new Vector3(10,10,1);
        // dManager.counter = 0;
    }
}
