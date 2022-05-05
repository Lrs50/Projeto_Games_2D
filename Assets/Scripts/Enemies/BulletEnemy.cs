using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    float startTime;
    float liveTime = 1f;
    Vector2 direction;
    bool go = false;
    public float damage = 5;
    public Sprite body;
    public Sprite[] breakAnimation;
    public GameObject explosion;
    bool done = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = body;
        transform.localScale *=5;
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
                StartCoroutine(Break());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "World"){
            StartCoroutine(Break());
        }
    }
    IEnumerator Break(){
        if(!done){
            done = true;
            yield return new WaitForSeconds(0.05f);
            Destroy(spriteRenderer);
            GameObject explosionAnimation = (GameObject) Instantiate(explosion,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(explosionAnimation);
            Destroy(gameObject);  
        }
    }
}
