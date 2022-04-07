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
    public float stamina = 100f;
    Image[] staminaImages;
    //public Text debug;

    //Sprite references
    public Sprite[] staminaUI;

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
        staminaImages = new Image[5];
        Transform stats = playerUI.transform.GetChild(0);
        Transform stamina = stats.GetChild(2);
        for(int i=0;i<5;i++){
            staminaImages[i] = stamina.GetChild(i+1).gameObject.GetComponent<Image>();
        }
        
    }
    // Start is called before the first frame update
    void Start() {

        currentState = idleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update() {
        currentState.UpdateState(this);
        
        stamina+= 2f*Time.deltaTime;

        UpdateStaminaUI();

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

   private void UpdateStaminaUI(){

       Sprite full = staminaUI[2];
       Sprite half = staminaUI[1];
       Sprite empty = staminaUI[0];



       for(int i=0;i<5;i++){
           if((stamina<(i+1)*20)){
               if(stamina>i*20 +10) 
                    staminaImages[i].sprite = half;
                else
                    staminaImages[i].sprite = empty;
           }else{
               staminaImages[i].sprite = full;
           }
       }

        

   }
}
