using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxVertical : MonoBehaviour
{
    private float length, startpos;
    public float parallaxFactor;
    public GameObject cam;
    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    // Update is called once per frame
    void Update()
    {
        float temp     = cam.transform.position.y * (1 - parallaxFactor);
        float distance = cam.transform.position.y * parallaxFactor;
    
        Vector3 newPosition = new Vector3(transform.position.x, startpos + distance, transform.position.z);
    
        transform.position = newPosition;
    
        if (temp > startpos + (length / 2))    startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }
}
