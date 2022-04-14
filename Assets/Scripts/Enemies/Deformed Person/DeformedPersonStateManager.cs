using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedPersonStateManager : EnemiesStateManager
{
    public override EnemiesSearchState searchState {get; protected set;} = new DeformedPersonSearchState();
    public override EnemiesMoveState moveState {get; protected set;} = new DeformedPersonMoveState();
//     public Transform target;
//     public float baseSpeed = 5f;
//     public float maxSpeed = 5f;
//     public float rotationSpeed = 8f;
//     public float maxRange;
//     public float minRange;

//     BaseStateEnemies currentState;

//     public DeformedPersonSearchState searchState = new DeformedPersonSearchState();
//     public DeformedPersonMoveState moveState = new DeformedPersonMoveState();

//     public Vector2 walkInput;
//     public LayerMask obstacles;
//     public Rigidbody2D rb;
//     Start is called before the first frame update
//     void Start() {

//         target = GameObject.FindWithTag("Player").transform;
//         currentState = searchState;

//         currentState.EnterState(this);
//     }

//     // Update is called once per frame
//     void Update() {
//         currentState.UpdateState(this);        
//     }

//     void FixedUpdate() {
//         currentState.FixedUpdateState(this);
//     }

//     public void SwitchState(BaseStateEnemies state){
//         currentState = state;
//         currentState.EnterState(this);
//     }

//     public void OnWalk(InputAction.CallbackContext context) {
//         walkInput = context.ReadValue<Vector2>();
//     }
    
//     public void OnShoot(InputAction.CallbackContext context) {
//     }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//    }
}
