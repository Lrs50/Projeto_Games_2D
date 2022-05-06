using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text npcNameText;
    public Text dialogueText;

    public Image overlay;

    public Text overlayText;

    public Animator animator;
    
    public Queue<string> sentences;

    public DialogueTrigger trigger;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
         if (Input.GetButtonDown("Fire3")) {
            DisplayNextSentence(); 
         }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger) {
        this.trigger = trigger;
        animator.SetBool("isOpen", true);

        npcNameText.text = dialogue.npcName;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0){
            StartCoroutine(EndDialogue());
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015f);
        }
    }

    IEnumerator EndDialogue() {
        animator.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.75f);
        if (this.trigger != null){
            trigger.Reset();
        }
    }

    public void ShowOverlay() {
        overlay.enabled = true;
        overlayText.enabled = true;
    }

    public void HideOverlay() {
        overlay.enabled = false;
        overlayText.enabled = false;
    }
}
