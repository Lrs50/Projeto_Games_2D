using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public bool interactable = false;

    public bool interacting = false;

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
        interacting = true;
    }

    void Update() {
        if (interactable && !interacting) {
            if (Input.GetButton("Fire3")){
                TriggerDialogue();
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
    }
}
