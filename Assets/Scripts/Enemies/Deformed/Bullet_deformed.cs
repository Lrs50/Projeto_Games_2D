using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_deformed : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    float startTime;
    float liveTime = 1f;
    Vector2 direction;
    bool go = false;
    public float damage = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setDestination(Vector2 goal){
        startTime = Time.time;

        Vector2 direction = goal;
        direction = new Vector2(direction.x,direction.y);
        
        direction = direction.normalized;
        rb.velocity = direction*speed;
        go=true;

    }

    // Update is called once per frame
    void Update()
    {
        if(go){
            if((Time.time-startTime)>=liveTime){
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "World"){
            Destroy(gameObject);
        }
    }
}
