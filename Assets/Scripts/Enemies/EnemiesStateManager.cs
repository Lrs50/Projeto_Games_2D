using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System;

public abstract class EnemiesStateManager : MonoBehaviour
{

    public Transform target;
    public float baseSpeed = 5f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 8f;
    public float maxRange;
    public float minRange;

    BaseStateEnemies currentState;

    public virtual EnemiesSearchState searchState {get; protected set;} = new EnemiesSearchState();
   
    public Vector2 walkInput;
    public LayerMask obstacles;
    public Rigidbody2D rb;
    public NavMeshAgent agent;
    public SpriteRenderer spriteRenderer;

    public Boolean isPatrol;
    public float angle=0;
    public float aggro;

    public float waitTime;

    public float startWaitTime;
    public string animationState = "idle";
    public Transform reference;
    public int direction = 0;

    // Start is called before the first frame update
    void Start() {
        //a
        reference = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

   public virtual void BecomeAggresive(){
       
   }

   public virtual void Animate(){

   }
   public void setDirection(){
       

        if(angle>-90 && angle<30){
            //front right
            direction = 1;
        }else if(angle<-90 && angle>-180){
            //front left
            direction = 0;
        }else if(angle<180 && angle>90){
            //back left
            direction = 2;
        }else if(angle<90 && angle>30){
            direction = 3;
        }
    }

}
