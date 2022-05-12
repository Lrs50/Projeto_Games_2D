using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

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
    float attackAnimationCooldown = 0.2f;
    public bool attackFlag = false;
    float attackCounter =0;
    float skill1Counter =0;
    float skill1Cooldown = 0.5f;
    float skill2Counter =0;
    float skill2Cooldown = 10;
    bool skill2flag = false;
    public float damageMultiplier = 1;

    BaseStatePlayer currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerDashState dashState = new PlayerDashState();
    public PlayerEvolveState evolveState= new PlayerEvolveState();
    public PlayerDeathState deathState = new PlayerDeathState();

    public Rigidbody2D rb;
    public Transform feet;

    //UI
    public GameObject playerUI;
    public GameObject pauseUI;
    public GameObject deathUI;
    public float stamina = 100f;
    Image[] staminaImages;
    Image[] healthImages;
    Image[] manaImages;
    //public Text debug;

    //Sprite references
    public Sprite[] elementsUI;
    public Material flash;


    //Shooting
    public Transform shootingOrigin;
    public GameObject defaultBullet;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;

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
    public Sprite[] wingsAnimation;
    public Sprite[] death1;
    public Sprite[] death2;
    public Sprite[] death3;
    public Sprite[] abilities;
    public Sprite[] itens;

    public GameObject wings;
    public SpriteRenderer wingsSR;

    //Enemy Information
    public Transform enemy;
    bool evolved = false;

    //Animation config
    public SpriteRenderer spriteRenderer;
    int animationSpeed = 7;
    public int animationFrame=0;
    int animationCount=0;
    public int numFrames = 4;
    public int animationOrientation = 0;
    public float health =100;
    public float mana = 100;
    public String animationMode = "2";
    bool skill3Flag = false;

    /**
    *   Items
    */
    public Image guaranaImg;
    public Text guaranaQtyText;
    public int guaranaQty;

    public Image jabuticabaImg;
    public Text jabuticabaQtyText;
    public int jabuticabaQty;

    private bool canHeal = true;

    //world manipulation
    public Transform enemyBarrier;
    public Transform enemyGroup;
    public bool nextStage = false;

    void CheckWorldEnemies(){
        int i = 0;
        if(enemyBarrier != null && enemyGroup != null){
            foreach(Transform child in enemyGroup){
                if(!(child.childCount > 0)){
                    Destroy(child.gameObject);
                    Destroy(enemyBarrier.GetChild(i).gameObject);
                }
                i++;
            }
        }
    }

    public void SetAnimationMode(){
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

        if(animationMode=="1"||animationMode=="2"||wings.activeSelf){
            Image logo = playerUI.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            logo.sprite = abilities[0];
            if(animationMode=="2"){
                logo = playerUI.transform.GetChild(1).GetChild(1).GetComponent<Image>();
                logo.sprite = abilities[1];
                if(wings.activeSelf){
                    logo = playerUI.transform.GetChild(1).GetChild(2).GetComponent<Image>();
                    logo.sprite = abilities[2];
                }
            }
        }
        
    }

    private void Awake()
    {



        Time.timeScale=1f;
        FindObjectOfType<DialogueManager>().HideOverlay();
        SetAnimationMode();
        spriteRenderer = GetComponent<SpriteRenderer>();
        wingsSR = wings.GetComponent<SpriteRenderer>();
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
        
        guaranaImg=playerUI.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        guaranaQtyText=playerUI.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        jabuticabaImg=playerUI.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
        jabuticabaQtyText=playerUI.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
    }
    
    void Start() {
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update() {
        currentState.UpdateState(this);
        
        // if(Input.GetKeyDown(KeyCode.E)){
        //     SwitchState(evolveState);
        // }
        if(Input.GetKeyDown(KeyCode.Return)){
            Pause(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause(false);
        }
        // if(Input.GetKeyDown(KeyCode.B)){
        //     rb.velocity = new Vector3(0,5,0);
        //     Collider2D temp = GetComponent<Collider2D>();
        //     temp.enabled = false;
        //     rb.isKinematic = true;
        // }

        UpdateUI();

        if(stamina>=100f){
            stamina=100f;
        }
        if(health<=0 && currentState!=deathState){
            health=0;
            SwitchState(deathState);
        }  
    }

    void FixedUpdate() {
    
        if(skill3Flag){
            return;
        }

        CheckWorldEnemies();

        if(attackFlag){
            attackCounter += 1/(50*attackAnimationCooldown);
            if(attackCounter>=1){
                attackFlag=false;
            }
        }

        if(skill2flag){
            if(skill2Counter<1){
                skill2Counter+= 2/(50*skill2Cooldown);
                
            }else{
                damageMultiplier = 1;
                skill2Counter = 0;
                skill2flag = false;
                bullet = defaultBullet;
                maxSpeed = 5;
                spriteRenderer.color = Color.white;
                
            }
        }

        if(shootCounter<1){
            shootCounter += 1/(50*shootCooldown);
        }

        
        if(skill1Counter<=1){
            skill1Counter+= 1/(50*skill1Cooldown);
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
        
        if(animationOrientation==3){
            wingsSR.sortingOrder=spriteRenderer.sortingOrder+1;
        }else{
            wingsSR.sortingOrder=spriteRenderer.sortingOrder-1;
        }


        animationCount++;
        if(animationCount==animationSpeed){
            animationCount=0;
            animationFrame++;
        }

        if(animationFrame>=numFrames){
            animationFrame=0;
        }

        currentState.FixedUpdateState(this);        
        
        if(enemy!=null){
            if(enemy.childCount==0 && evolved==false){
                evolved=true;
                //if(Scene.name==) 
                SwitchState(evolveState);
            }
        }

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
        if(stamina<5f){
            sprintInput=0;
        }
    }

    public void OnSkill1(InputAction.CallbackContext context) {
        if(context.ReadValue<float>()!=0 && (animationMode=="1"||animationMode=="2") && shootCounter>=1 && skill1Counter>=1 && mana>=10 && Time.timeScale>=0.5){
            mana -=10;
            attackFlag = true;
            skill1Counter = 0;
            attackCounter = 0;
            Instantiate(bullet2,shootingOrigin.position,Quaternion.identity);
        }
    }

    public void OnSkill2(InputAction.CallbackContext context) {
        if(context.ReadValue<float>()!=0 && animationMode=="2" && !skill2flag && mana>=50 && Time.timeScale>=0.5){
            mana -=50;
            damageMultiplier = 0.5f;
            skill2flag = true;
            defaultBullet = bullet;
            bullet = bullet3;
            spriteRenderer.color = new Color(153f/255f,255f/255f,239f/255f,1);
            maxSpeed *= 1.25f;
           
        }
    }

    public void OnSkill3(InputAction.CallbackContext context) {
        Scene scene = SceneManager.GetActiveScene();
        if(animationMode=="2" && scene.name=="Phase1_2" && context.ReadValue<float>()!=0 && wings.activeSelf && skill3Flag==false && Time.timeScale>=0.5){
            skill3Flag=true;
            StartCoroutine(Skill3());
        }
    }

    public void OnDash(InputAction.CallbackContext context) {
        if(Time.timeScale>=0.5) dashInput = context.ReadValue<float>();
    }
    
    public void OnShoot(InputAction.CallbackContext context) {
        attackFlag = true;
        attackCounter = 0;
        if(context.ReadValue<float>()!=0 && shootCounter>=1 && Time.timeScale>=0.5){
            shootCounter=0;
            Instantiate(bullet,shootingOrigin.position,Quaternion.identity);
        }
        
    }

    public void OnHeal(InputAction.CallbackContext context) {
        if(context.ReadValue<float>()!=0 && Time.timeScale>=0.5){
            if (guaranaQty > 0 && canHeal){
                Heal(50);
            }
        }
    }

    public void OnHealMana(InputAction.CallbackContext context) {
         if(context.ReadValue<float>()!=0 && Time.timeScale>=0.5){
             if (jabuticabaQty > 0 && canHeal){
                HealMana(50);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Guarana")) {
            GetGuarana();
        }

        if(other.gameObject.tag.Equals("Coco")) {
            GetCoco();
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.tag != "Interactable") return;

        canHeal = false;  
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag != "Interactable") return;

        canHeal = true;
    }

    private void GetGuarana() {
        guaranaQty += 1;
    }

    private void GetCoco() {
        jabuticabaQty += 1;
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

        if (guaranaQty <= 0){
            guaranaQtyText.enabled = false;
            guaranaImg.color = new Vector4(50/255f, 50/255f, 50/255f, 0.7f);
        }
        else {
            guaranaQtyText.enabled = true;
            guaranaQtyText.text = ""+guaranaQty;
            guaranaImg.color = Color.white;
        }

        if (jabuticabaQty <= 0) {
            jabuticabaQtyText.enabled = false;
            jabuticabaImg.color = new Vector4(50/255f, 50/255f, 50/255f, 0.7f);
        }
        else {
            jabuticabaQtyText.enabled = true;
            jabuticabaQtyText.text = ""+jabuticabaQty;
            jabuticabaImg.color = Color.white;
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

    public void Heal(float amount){
       health += amount;
       guaranaQty -= 1;
       if (health > 100){
           health = 100;
       }
       StartCoroutine(HealAnimation());
    }

    IEnumerator HealAnimation(){
        spriteRenderer.color=new Vector4(0/255f, 255/255f, 0/255f,0.7f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }

    public void HealMana(float amount){
        mana += amount;
        jabuticabaQty -= 1;
        if (mana > 100){
            mana = 100;
        }
        StartCoroutine(HealManaAnimation());
    }

    IEnumerator HealManaAnimation(){
        spriteRenderer.color=new Vector4(0/255f, 255/255f, 255/255f, 0.7f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }

    IEnumerator Evolve(){
        animationOrientation = 0;
        spriteRenderer.sprite = idleAnimation[0];
        yield return new WaitForSeconds(0.5f);
        
        Material old = spriteRenderer.material;
        Vector3 escale = transform.localScale;
        Vector3 pos = transform.position;
        float animationCooldown=0.1f;
        spriteRenderer.material=flash;

        for(int i=0;i<10;i++){
            spriteRenderer.color=Color.white;
            transform.localScale = escale*1.25f;
            transform.position = pos + (Vector3)UnityEngine.Random.insideUnitCircle.normalized*0.2f;
            yield return new WaitForSeconds(animationCooldown);
            transform.localScale = escale*1f;
            spriteRenderer.color=UnityEngine.Random.ColorHSV();
            transform.position = pos + (Vector3)UnityEngine.Random.insideUnitCircle.normalized*0.2f;
            yield return new WaitForSeconds(animationCooldown);
            transform.localScale = escale*0.8f;
            spriteRenderer.color=Color.black;
            yield return new WaitForSeconds(animationCooldown);
            transform.position = pos + (Vector3)UnityEngine.Random.insideUnitCircle.normalized*0.2f;
        }

        if(animationMode=="normal"){
            animationMode="1";
            Image logo = playerUI.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            logo.sprite = abilities[0];
            SetAnimationMode();
        }else if(animationMode=="1"){
            Image logo = playerUI.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            logo.sprite = abilities[1];
            animationMode="2";
            SetAnimationMode();
        }else if(animationMode=="2"){
            Image logo = playerUI.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            logo.sprite = abilities[2];
            wings.SetActive(true);
        }

        transform.localScale = escale;
        spriteRenderer.material = old;
        spriteRenderer.color=Color.white;
         transform.position = pos;
        SwitchState(idleState);

    }

    public void StartEvolve(){
        rb.velocity=Vector2.zero;
        StartCoroutine(Evolve());
    }

    public void Pause(bool pause){
        
        if(pause){
            pause=false;
            Time.timeScale=1;
            pauseUI.SetActive(false);
        }else{
            pause=true;
            Time.timeScale=0;
            pauseUI.SetActive(true);
        }
    }

    IEnumerator Skill3(){
        rb.velocity = new Vector3(0,5,0);
        Collider2D temp = GetComponent<Collider2D>();
        temp.enabled = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(10f);
        rb.velocity = new Vector3(0,0,0);
        yield return new WaitForSeconds(1f);
        nextStage=true;
    }

    public void setWings(bool set){
        wings.SetActive(set);
        if(animationMode=="1"||animationMode=="2"||wings.activeSelf){
            Image logo = playerUI.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            logo.sprite = abilities[0];
            if(animationMode=="2"){
                logo = playerUI.transform.GetChild(1).GetChild(1).GetComponent<Image>();
                logo.sprite = abilities[1];
                if(wings.activeSelf){
                    logo = playerUI.transform.GetChild(1).GetChild(2).GetComponent<Image>();
                    logo.sprite = abilities[2];
                }
            }
        }
    }

}

