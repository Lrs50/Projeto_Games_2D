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
    void Start()
    {
        startTime = Time.time;
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        Vector3 sourceToDestination = player.transform.position - boss.transform.position;
        motion = sourceToDestination.normalized * _projectileSpeed;
    }
    void Update()
    {
        if((Time.time-startTime)>=liveTime){
            Destroy(gameObject);
        }
        transform.position += motion * Time.deltaTime;   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Target" || other.gameObject.name == "Collider" || other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}