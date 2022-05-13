using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public bool interactable = false;

    public bool interacting = false;
    [SerializeField] private bool isMain;
    public bool done;

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
        interacting = true;
    }

    void Start()
    {
        done = false;
    }

    void Update() {
        if(isMain && !done){
            TriggerDialogue(); 
            done = true;           
        }
        if (interactable && !interacting && !isMain) {
            if (Input.GetButtonDown("Fire3")){
                TriggerDialogue();
            }  
        }

        if (interacting){
            if(Vector3.Distance(FindObjectOfType<PlayerStateManager>().transform.position, transform.position) >= 10){
                FindObjectOfType<DialogueManager>().FinishDialogue();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.tag != "Player") return;

        interactable = true;       
        FindObjectOfType<DialogueManager>().ShowOverlay();     
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag != "Player") return;

        interactable = false;
        FindObjectOfType<DialogueManager>().HideOverlay();
    }

    public void Reset(){
        interacting = false;
        done = true;
    }
}
