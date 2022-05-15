using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerdeMov : MonoBehaviour
{
    public GameObject cam;
    public bool triggered;
    void Start()
    {
        
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.transform.position.y > transform.position.y){
            transform.position = new Vector3(transform.position.x,transform.position.y+(0.2f*Time.deltaTime),transform.position.z);
        }else{
            triggered = true;
        }
    }
}
