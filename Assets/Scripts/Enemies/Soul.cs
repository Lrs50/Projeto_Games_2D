using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

    public Transform target;
    public Rigidbody2D rb;
    public float distanceToPlayer;
    public float intensity;

    Vector3 pullForce;
    
    void Start() {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        distanceToPlayer = Vector3.Distance(target.position, transform.position);
        pullForce = (target.position - transform.position).normalized / distanceToPlayer * intensity;
        rb.AddForce(pullForce, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Player")) {
            Destroy(gameObject);
        }
   }
}
