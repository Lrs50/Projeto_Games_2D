using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCounter : MonoBehaviour
{
    private DialogueManager dManager;
    private float fadeOutTime = 1f;
    [SerializeField ]private GameObject toFade;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(dManager.counter >= 3){
            //Change
            Debug.Log("Passar cena");
            StartCoroutine(DoFadeOut(toFade));
            dManager.counter = 0;
        }
    }

    private IEnumerator DoFadeOut(GameObject toFade){
        Debug.Log("aaa");
        foreach(Transform child in toFade.transform)
        {
            SpriteRenderer tmpRenderer = child.gameObject.GetComponent<SpriteRenderer>();
            Color tmpColor = tmpRenderer.color;
            while(tmpColor.a < 1f){
                tmpColor.a += Time.deltaTime / fadeOutTime;

                if(tmpColor.a >= 1f){
                    tmpColor.a = 1.0f;
                }
                yield return null;
            }
            tmpRenderer.color = tmpColor;
        }
        //Color tmpColor = _sprite.color;
    }
}
