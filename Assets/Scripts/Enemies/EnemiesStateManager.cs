using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System;

public class EnemiesStateManager : MonoBehaviour
{

    public Transform target;
    public float baseSpeed = 5f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 8f;
    public float maxRange;
    public float minRange;

    BaseStateEnemies currentState;

    public virtual EnemiesSearchState searchState {get; protected set;} = new EnemiesSearchState();
    public virtual EnemiesMoveState moveState {get; protected set;} = new EnemiesMoveState();
    public virtual EnemiesAttackState attackState {get; protected set;} = new EnemiesAttackState();
    
    public Vector2 walkInput;
    public LayerMask obstacles;
    public Rigidbody2D rb;
    public NavMeshAgent agent;

    public Boolean isPatrol;

    public float aggro;

    public float waitTime;

    public float startWaitTime;

    // Start is called before the first frame update
    void Start() {
        //a
        target = GameObject.FindWithTag("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentState = searchState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update() {
        currentState.UpdateState(this);        
    }

    void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(BaseStateEnemies state){
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnWalk(InputAction.CallbackContext context) {
        walkInput = context.ReadValue<Vector2>();
    }
    
    public void OnShoot(InputAction.CallbackContext context) {
    }

   private void OnTriggerEnter2D(Collider2D other)
   {
   }
}