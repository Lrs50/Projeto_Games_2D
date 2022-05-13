using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemFakeParallax : MonoBehaviour
{
    private float length, startpos;
    public float parallaxFactor;
    public GameObject cam;
    // Update is called once per frame
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(startpos);
        Debug.Log(length);
    }
    void Update()
    {
        float temp     = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;
        transform.position = new Vector3(startpos+distance, transform.position.y, transform.position.z);
        //transform.position = newPosition;
    
        if (temp > startpos + (length / 2)){
            startpos += length;
        }else if (temp < startpos - (length / 2)){
            startpos -= length;
        }
    }
}
