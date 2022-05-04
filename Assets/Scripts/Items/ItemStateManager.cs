using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStateManager : MonoBehaviour
{
   
    public Transform player;
    public Rigidbody2D rb;

    public float pickupRange;

    public float dropForwardForce, dropUpwardForce;

    public static bool inventoryFull;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Player") && !inventoryFull) {
            Destroy(gameObject);
        }
   }

}
