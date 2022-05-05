using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    float startTime;
    float liveTime = 1f;
    public float damage = 1;
    public Sprite body;
    public Sprite[] breakAnimation;
    public GameObject explosion;
    bool done = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = body;
        transform.localScale *=5;

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
            StartCoroutine(Break());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "World" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Target"){
            StartCoroutine(Break());
        }
    }

    IEnumerator Break(){
        if(!done){
            done = true;
            Destroy(spriteRenderer);
            GameObject explosionAnimation = (GameObject) Instantiate(explosion,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(explosionAnimation);
            Destroy(gameObject);  
        }
    }
    
}
