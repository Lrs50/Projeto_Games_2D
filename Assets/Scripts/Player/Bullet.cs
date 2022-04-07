using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    float startTime;
    float liveTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(direction.x - transform.position.x,direction.y-transform.position.y);
        
        direction = direction.normalized;
        rb.velocity = direction*speed;
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time-startTime)>=liveTime){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="Target" || other.gameObject.name=="Collider"){
            Destroy(gameObject);
        }
    }
}
