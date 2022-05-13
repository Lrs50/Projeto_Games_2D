using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class condActivateTrigger : MonoBehaviour
{
    public DialogueTrigger dTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dTrigger = GetComponent<DialogueTrigger>();
        StartCoroutine(triggerAfterDelay());

    }

    IEnumerator triggerAfterDelay(){
        yield return new WaitForSeconds(2f);
        dTrigger.isMain = true;
    }
}
