using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigIndian : MonoBehaviour
{
   
    private DialogueTrigger dialogueTrigger;

    public Dialogue newDialogue;

    private bool dropped = false;

    void Start() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    
    void Update(){
        if (dialogueTrigger.done && !dropped) {
            GameObject.Find("Player").GetComponent<PlayerStateManager>().GetGuaranaX(3);
            dialogueTrigger.dialogue = newDialogue;
            dropped = true;
        }
    }
}
