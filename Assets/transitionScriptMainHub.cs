using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class transitionScriptMainHub : MonoBehaviour
{
    public Animator animator;
    public GameObject folhas;
    public MovUpFolhas folhasScript;
    // Start is called before the first frame update
    void Start()
    {
        folhasScript = folhas.GetComponent<MovUpFolhas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(folhasScript.canMove){
            fadeOut();
        }
    }
    public void fadeOut(){
        animator.SetTrigger("fadeOut");
    }

    public void fadeIn(){
        animator.SetTrigger("fadeIn");
    }

    public void onFadeCompleteFirst(){
        SceneManager.LoadScene("MainHub",LoadSceneMode.Single);
        fadeIn();
    }
}
