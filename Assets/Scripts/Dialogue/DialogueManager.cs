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
    public bool dialogueIsOver;
    public int counter;
    public bool bloquearDialogo;

    public Image arrow;

    void Start()
    {
        counter = 0;
        dialogueIsOver = false;
        sentences = new Queue<string>();
        bloquearDialogo = false;
    }

    void Update()
    {
         if (Input.GetButtonDown("Fire3") && !bloquearDialogo) {
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
        counter++;
        if (sentences.Count == 0){
            StartCoroutine(EndDialogue());
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence){
        arrow.enabled = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015f);
        }

        arrow.enabled = true;
    }

    public void FinishDialogue(DialogueTrigger trigger) {
        dialogueIsOver = true;
        sentences = new Queue<string>();
        StopAllCoroutines();
        StartCoroutine(EndDialogue());
        trigger.Reset();
    }

    IEnumerator EndDialogue() {
        this.dialogueIsOver = true;
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
