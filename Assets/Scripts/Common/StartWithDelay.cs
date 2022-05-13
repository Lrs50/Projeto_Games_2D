using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWithDelay : MonoBehaviour
{
    // Start is called before the first frame update
    public float seconds;
    void Start()
    {
        StartCoroutine(StartAfterSeconds(seconds));
    }


    IEnumerator StartAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds);
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
    }

}
