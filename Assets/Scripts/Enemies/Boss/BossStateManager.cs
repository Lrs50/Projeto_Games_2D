using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
public class BossStateManager : MonoBehaviour{
    public Transform target;
    BaseStateBoss currentState;
    public BossSearchState searchState = new BossSearchState();
    public BossAttackState attackState = new BossAttackState();
    public BossFlyState flyingState = new BossFlyState();
    public BossLandingState landingState = new BossLandingState();
    public BossDashAttackState dashAttack = new BossDashAttackState();
    public BossTrackingAttackState trackAttack = new BossTrackingAttackState();
    public BossNormalAttackState normalAttack = new BossNormalAttackState();
    public BossDrillAttackState drillAttack = new BossDrillAttackState();
    public BossGoingBackState backState = new BossGoingBackState();
    public GameObject bullet;
    public GameObject trackingBullet;
    public GameObject normalBullet;
    public GameObject instanceOfTrackingBullet;
    public FeatherProjectile bulletProperties;
    public TrackingProjectile trackingBulletProperties;
    public NormalProjectile normalBulletProperties;
    public float maxHealth;
    public float currHealth;
    public float damage = 15f;
    public float flyAttackDamage = 20f;
    public float minAttackInterval;
    public float maxAttackInterval;
    public bool rageMode;
    public float baseSpeed = 5f;
    public SpriteRenderer spriteRenderer;
    public Sprite shadowSprite;
    public Sprite bossSprite;
    public float startFollowingTime;
    public float followingTime = 5f;
    public float followingDistance;
    public Rigidbody2D rb;
    public Collider2D cd;
    public float dashMag;
    public float dashTimer;
    public float timerForAttacks;
    public float flySpeed;
    public float searchSpeed;
    public float startDelayDashAttack;
    public float delayToDashAttack;
    public BossShadow shadow;
    public GameObject instanceOfShadow;
    public int qtdDashDrillAttack;
    public int qtdDash;

    public Slider health_slider;
    public int dashCounter = 0;

    public Vector3 goBack;
    // Sprites

    public Sprite[] animations1;
    public Sprite[] animations2; 
    public Sprite[] wings;

    [System.NonSerialized] public Sprite[] idleAnimation;
    [System.NonSerialized] public Sprite[] simpleAttackAnimation1;
    [System.NonSerialized] public Sprite[] simpleAttackAnimation2;
    [System.NonSerialized] public Sprite[] Animation360;
    [System.NonSerialized] public Sprite[] transformAnimation;
    [System.NonSerialized] public Sprite[] loopPreAttackAnimation;
    [System.NonSerialized] public Sprite[] initAttackAnimation;
    [System.NonSerialized] public Sprite[] attackAnimation;
    [System.NonSerialized] public Sprite[] undoTransformAnimation;

    [System.NonSerialized] public Sprite[] idleWingsAnimation;
    [System.NonSerialized] public Sprite[] Animation360Wings;
    [System.NonSerialized] public Sprite[] transformWingsAnimation;
    [System.NonSerialized] public Sprite[] undoTransWingsformAnimation;

    int counter =0;
    int animationTime=9;
    public int index = 0;
    public int indexWings = 0;

    public GameObject wings_object;
    public SpriteRenderer wingsSR;

    public void SetAnimation(){
        idleAnimation = new Sprite[8];
        simpleAttackAnimation1= new Sprite[8];
        simpleAttackAnimation2= new Sprite[8];
        Animation360 = new Sprite[8];
        transformAnimation= new Sprite[4];
        loopPreAttackAnimation= new Sprite[4];
        initAttackAnimation= new Sprite[2];
        attackAnimation= new Sprite[2];
        undoTransformAnimation= new Sprite[4];
        idleWingsAnimation= new Sprite[8];
        Animation360Wings= new Sprite[8];
        transformWingsAnimation= new Sprite[3];
        undoTransWingsformAnimation= new Sprite[3];

        wingsSR = wings_object.GetComponent<SpriteRenderer>();

        for(int i=0;i<wings.Length;i++){
            if(i<8){
                idleWingsAnimation[i]=wings[i];
            }else if(i<16){
                Animation360Wings[i-8]=wings[i];
            }else if(i<19){
                transformWingsAnimation[i-16]=wings[i];
            }else if(i<22){
                undoTransWingsformAnimation[i-19]=wings[i];
            }
        }

        for(int i=0;i<animations1.Length;i++){
            if(i<8){
                idleAnimation[i]=animations1[i];
            }else if(i<16){
                simpleAttackAnimation1[i-8]=animations1[i];
            }else if(i<24){
                simpleAttackAnimation2[i-16]=animations1[i];
            }else if(i<32){
                Animation360[i-24]=animations1[i];
            }
        }
        for(int i=0;i<animations2.Length;i++){
            if(i<4){
                transformAnimation[i]=animations2[i];
            }else if(i<8){
                loopPreAttackAnimation[i-4]=animations2[i];
            }else if(i<10){
                initAttackAnimation[i-8]=animations2[i];
            }else if(i<12){
                attackAnimation[i-10]=animations2[i];
            }else if(i<16){
                undoTransformAnimation[i-12]=animations2[i];
            }
        }
    }

    void Start()
    {
        minAttackInterval = 1f;
        maxAttackInterval = 2f;
        rageMode = false;
        currHealth = maxHealth;
        SetAnimation();
        Physics2D.IgnoreLayerCollision(2,7);
        target = GameObject.FindWithTag("Player").transform;
        bulletProperties = bullet.GetComponent<FeatherProjectile>();
        shadow = instanceOfShadow.GetComponent<BossShadow>();
        trackingBulletProperties = trackingBullet.GetComponent<TrackingProjectile>();
        normalBulletProperties = normalBullet.GetComponent<NormalProjectile>();
        currentState = searchState;
        currentState.EnterState(this);
        
    }

     // Update is called once per frame
    void Update() {
        currentState.UpdateState(this);        
    }

    void FixedUpdate() {
        counter++;
        if(counter>=animationTime){
            counter=0;
            index++;
            indexWings++;
        }
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(BaseStateBoss state){
        currentState = state;
        currentState.EnterState(this);
    }

    public void AreaDamage(Vector2 center, float radius)
    {
        bool hitPlayer = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        for (int i = 0; i < hitColliders.Length && !hitPlayer; i++)
        {
            if(hitColliders[i].CompareTag("Player")){
                //Debug.Log("Acertou o player");
                PlayerStateManager temp = target.GetComponent<PlayerStateManager>();  
                temp.TakeDamage(flyAttackDamage);          
                hitPlayer = true;
            }
        }
        // foreach (var hitCollider in hitColliders)
        // {
        //     if(hitCollider.CompareTag("Player")){
        //         Debug.Log("Acertou o player");
        //     }
        // }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            this.rb.velocity=Vector2.zero;
            
        }
        if(other.gameObject.tag == "Player Attack"){
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            currHealth -= bullet.damage;
            //Debug.Log(currHealth);
            health_slider.value = currHealth/maxHealth;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player Attack"){
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            currHealth -= bullet.damage;
            //Debug.Log(currHealth);
            health_slider.value = currHealth/maxHealth;
            StartCoroutine(DamageAnimation());
        }
    }

    IEnumerator DamageAnimation(){
        
        spriteRenderer.color=new Vector4(255/255f, 0/255f, 0/255f,0.3f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }
    

}