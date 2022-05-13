using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeObject : MonoBehaviour
{
    private float length, startpos;
    public GameObject nuvem;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = nuvem.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > length/2){
            transform.position = new Vector3(startpos,transform.position.y,transform.position.z);
        }else{
            transform.position = new Vector3(transform.position.x+((0.5f*0.075f)*Time.deltaTime),transform.position.y,transform.position.z);
        }
    }
}
