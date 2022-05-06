using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : MonoBehaviour
{
    [SerializeField,Min(1)] public int _numberOfProjectiles;
    [SerializeField,Min(1)] public float _projectileSpeed;
    [SerializeField] public GameObject _projectilePrefab;
    [SerializeField] public GameObject _enemyPrefab;
    [SerializeField, Range(0,2)] public float _spawnRadius = 0.2f;

    public Vector3 _bossPosition;
    public float startTime;
    public float liveTime = 1f;
    private Vector3 motion;
    public SpriteRenderer spriteRenderer;
    public float damage = 10;
    public Sprite body;
    public Sprite[] breakAnimation;
    public GameObject explosion;
    bool done = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = body;
        startTime = Time.time;
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        Vector2 direction = (transform.position - player.transform.position).normalized;
        var angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        var offset = 90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        Vector3 sourceToDestination = player.transform.position - boss.transform.position;
        motion = sourceToDestination.normalized * _projectileSpeed;
    }
    void Update()
    {
        if((Time.time-startTime)>=liveTime){
            StartCoroutine(Break());
        }
        transform.position += motion * Time.deltaTime;   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "World" || other.gameObject.tag == "Player")
        {
            StartCoroutine(Break());
        }
    }
    IEnumerator Break(){
        if(!done){
            done = true;
            yield return new WaitForSeconds(0.02f);
            Destroy(spriteRenderer);
            GameObject explosionAnimation = (GameObject) Instantiate(explosion,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(explosionAnimation);
            Destroy(gameObject);  
        }
    }
}