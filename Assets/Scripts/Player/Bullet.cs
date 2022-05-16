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
    public bool done = false;
    public int health = 1;
    public Vector2 direction;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = body;
        transform.localScale *=5;

        startTime = Time.time;

        direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(direction.x - transform.position.x,direction.y-transform.position.y);
        
        direction = direction.normalized;
        SetStats();

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
        if(other.gameObject.tag == "Enemy"){
            CollisionAction();
        }
        if(other.gameObject.name == "World" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Target" || other.gameObject.tag == "Collider" || other.gameObject.tag == "Boss"){
            health--;
            audioSource.Play();
            if(health<=0){
                StartCoroutine(Break()); 
            }
        }
    }

    virtual public IEnumerator Break(){
        if(!done){
            if(GetComponent<SpriteRenderer>().isVisible){
                audioSource.Play();
            }
            Collider2D temp= GetComponent<Collider2D>();
            Destroy(temp);
            done = true;
            Destroy(spriteRenderer);
            GameObject explosionAnimation = (GameObject) Instantiate(explosion,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(explosionAnimation);
            Destroy(gameObject);  
        }
    }

    virtual public void SetStats(){
        speed = 20f;
        liveTime = 1f;
        damage = 1;
    }
    virtual public void CollisionAction(){

    }
    
    public void rotateTowardsPlayer(Vector2 direction){
        var offset = 90f;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle+offset));
    }

}
