using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openTrigger : MonoBehaviour
{
    public Animator animator;
    private GameObject verde;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject image;
    private transitionScript checking;
    
    // Start is called before the first frame update
    void Start()
    {
        verde = transform.parent.gameObject;
        checking = image.GetComponent<transitionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checking.triggered){
            openEyes();
        }
    }
    public void openEyes(){
        animator.SetTrigger("opening");
    }

}
