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

    float shootCooldown = 0.2f;
    float shootCounter = 1;

    BaseStatePlayer currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerDashState dashState = new PlayerDashState();

    public Rigidbody2D rb;


    //UI
    public GameObject playerUI;
    public float stamina = 100f;
    Image[] staminaImages;
    Image[] healthImages;
    Image[] manaImages;
    //public Text debug;

    //Sprite references
    public Sprite[] elementsUI;


    //Shooting
    public Transform shootingOrigin;
    public GameObject bullet;

    public Vector2 walkInput;
    public float sprintInput;
    public float dashInput;

    public Vector2 currentInputVector;
    public Vector2 smoothInputVelocity;

    //Sprites
    public Sprite[] normalAnimation;
    public Sprite[] animation2;
    public Sprite[] animation3;

    [System.NonSerialized] public Sprite[] idleAnimation;
    [System.NonSerialized] public Sprite[] walkAnimation;
    [System.NonSerialized] public Sprite[] runAnimation;
    [System.NonSerialized] public Sprite[] dashAnimation;
    [System.NonSerialized] public Sprite[] idleAttackAnimation;
    [System.NonSerialized] public Sprite[] walkAttackAnimation;


    //Animation config
    public SpriteRenderer spriteRenderer;
    int animationSpeed = 7;
    public int animationFrame=0;
    int animationCount=0;
    public int numFrames = 4;
    public int animationOrientation = 0;
    public float health =100;
    public float mana = 100;

    public void SetAnimationMode(String animationMode){
        Sprite[] reference = normalAnimation;

        idleAnimation = new Sprite[8];
        walkAnimation =new Sprite[16];
        runAnimation = new Sprite[16];
        dashAnimation = new Sprite[4] ;
        idleAttackAnimation = new Sprite[16];
        walkAttackAnimation = new Sprite[32];

        if(animationMode.Equals("normal")){
            reference = normalAnimation;
        }else if(animationMode.Equals("1")){
            reference = animation2;
        }else if(animationMode.Equals("2")){
            reference = animation3;
        }

        for(int i=0;i<idleAnimation.Length;i++){
            idleAnimation[i] = reference[i];
        }
        for(int i=0;i<walkAnimation.Length;i++){
            walkAnimation[i] = reference[i+12];
        }
        for(int i=0;i<runAnimation.Length;i++){
            runAnimation[i] = reference[28+i];
        }
        for(int i=0;i<dashAnimation.Length;i++){
            dashAnimation[i] = reference[8+i];
        }
        for(int i=0;i<idleAttackAnimation.Length;i++){
            idleAttackAnimation[i] = reference[44+i];
        }
        for(int i=0;i<walkAttackAnimation.Length;i++){
            walkAttackAnimation[i] = reference[52+i];
        }
    }

    private void Awake()
    {
        SetAnimationMode("normal");
        spriteRenderer = GetComponent<SpriteRenderer>();

        staminaImages = new Image[4];
        healthImages = new Image[5];
        manaImages = new Image[5];

        Transform stats = playerUI.transform.GetChild(0);
        Transform health = stats.GetChild(0);
        Transform stamina = stats.GetChild(2);
        Transform mana = stats.GetChild(1);

        for(int i=0;i<5;i++){
            healthImages[i] = health.GetChild(i+1).gameObject.GetComponent<Image>();
        }

        for(int i=0;i<4;i++){
            staminaImages[i] = stamina.GetChild(i+1).gameObject.GetComponent<Image>();
        }

        for(int i=0;i<5;i++){
            manaImages[i] = mana.GetChild(i+1).gameObject.GetComponent<Image>();
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
        
        UpdateUI();

        if(stamina>=100f){
            stamina=100f;
        }
        if(health<=0){
            health=0;
        }  
    }

    void FixedUpdate() {

        if(shootCounter<1){
            shootCounter += 1/(50*shootCooldown);
        }

        if(walkInput.y>0){
            //UP
            animationOrientation = 3;
        }else if (walkInput.y<0){
            //Down
            animationOrientation = 0;
        }else if(walkInput.x>0){
            //Right
            animationOrientation = 1;
        }else if(walkInput.x<0){
            //left
            animationOrientation = 2;
        }
        
        animationCount++;
        if(animationCount==animationSpeed){
            animationCount=0;
            animationFrame++;
        }
        if(animationFrame==numFrames){
            animationFrame=0;
        }

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

        if(context.ReadValue<float>()!=0 && shootCounter>=1){
            shootCounter=0;
            Instantiate(bullet,shootingOrigin.position,Quaternion.identity);
        }
        
    }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.gameObject.name=="Door"){
           Loader.Load(Loader.Scene.Phase1_0);
       }

        if(other.gameObject.tag.Equals("Item")) {
            Debug.Log("got an item!");
        }
   }

   private void UpdateUI(){

       Sprite estaminaFull = elementsUI[6];
       Sprite estaminaHalf = elementsUI[7];
       Sprite estaminaEmpty = elementsUI[8];

       Sprite healthFull = elementsUI[0];
       Sprite healthHalf = elementsUI[1];
       Sprite healthEmpty = elementsUI[2];

       Sprite manaFull = elementsUI[3];
       Sprite manaHalf = elementsUI[4];
       Sprite manaEmpty = elementsUI[5];

        for(int i=0;i<5;i++){
           if((health<(i+1)*20)){
               if(health>i*20 +10) 
                    healthImages[i].sprite = healthHalf;
                else
                    healthImages[i].sprite = healthEmpty;
           }else{
               healthImages[i].sprite = healthFull;
           }
        }

        for(int i=0;i<5;i++){
           if((mana<(i+1)*20)){
               if(mana>i*20 +10) 
                    manaImages[i].sprite = manaHalf;
                else
                    manaImages[i].sprite = manaEmpty;
           }else{
               manaImages[i].sprite = manaFull;
           }
        }

        for(int i=0;i<4;i++){
           if((stamina<(i+1)*25)){
               if(stamina>i*25 +12) 
                    staminaImages[i].sprite = estaminaHalf;
                else
                    staminaImages[i].sprite = estaminaEmpty;
           }else{
               staminaImages[i].sprite = estaminaFull;
           }
        }
   }

   public void TakeDamage(float amount){
       health -= amount;
       StartCoroutine(DamageAnimation());
   }

    IEnumerator DamageAnimation(){
        
        spriteRenderer.color=new Vector4(255/255f, 0/255f, 0/255f,0.7f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }
}

