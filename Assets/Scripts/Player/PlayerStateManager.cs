using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PlayerStateManager : MonoBehaviour
{


    public float baseSpeed = 5f;
    public float maxSpeed = 5f;
    public float sprintSpeed = 0.5f;
    public float rotationSpeed = 8f;
    public float dashMag = 10f;
    public float dashTimer = 0.3f;

    BaseStatePlayer currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerDashState dashState = new PlayerDashState();

    public Rigidbody2D rb;


    //UI
    public GameObject playerUI;
    [NonSerialized] public Text staminaUI;
    public float stamina = 100f;


    //Shooting
    public Transform shootingOrigin;
    public GameObject bullet;

    public Vector2 walkInput;
    public float sprintInput;
    public float dashInput;

    public Vector2 currentInputVector;
    public Vector2 smoothInputVelocity;


    private void Awake()
    {
        staminaUI = playerUI.transform.GetChild(1).gameObject.GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start() {

        currentState = idleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update() {
        currentState.UpdateState(this);
        staminaUI.text = "Estâmina: " + Math.Round(stamina,1).ToString();
        
        stamina+= 2f*Time.deltaTime;
        if(stamina>=100f){
            stamina=100f;
        }
        
    }

    void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(BaseStatePlayer state){
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnWalk(InputAction.CallbackContext context) {
        walkInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        sprintInput = context.ReadValue<float>();
    }

    
    public void OnDash(InputAction.CallbackContext context) {
        dashInput = context.ReadValue<float>();
    }
    
    public void OnShoot(InputAction.CallbackContext context) {

        if(context.ReadValue<float>()!=0){
            Instantiate(bullet,shootingOrigin.position,Quaternion.identity);
        }
    }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.gameObject.name=="Door"){
           Loader.Load(Loader.Scene.Phase1Scene0);
       }
   }
}
