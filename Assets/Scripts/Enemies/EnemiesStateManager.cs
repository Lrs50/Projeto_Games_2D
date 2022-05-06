using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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
    public Slider healthBar;

    public Vector2 walkInput;
    public LayerMask obstacles;
    public Rigidbody2D rb;
    public NavMeshAgent agent;
    public SpriteRenderer spriteRenderer;

    public bool isPatrol;
    public float angle=0;
    public float aggro;

    public float waitTime;

    public float startWaitTime;
    public string animationState = "idle";
    public Transform reference;
    public int direction = 0;

    public float health = 0;
    public float damage = 0;
    public float maxHealth = 0;

    public float dropChance = 0.1f;

    public GameObject soul;

    public GameObject guarana;
    public float guaranaDropChance;
    public GameObject acai;
    public float acaiDropChance;
    public GameObject coco;
    public float cocoDropChance;

    // Start is called before the first frame update
    void Start() {
        //a
        SetProperties();
        maxHealth = health;
        healthBar = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>(); 

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
    
    public virtual void OnShoot(Vector3 direction) {

    }

    private void OnTriggerEnter2D(Collider2D other){
    
        if(other.gameObject.tag == "Player Attack"){
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            health -= bullet.damage;
            
            StartCoroutine(DamageAnimation());

            if(health<=0){
                MaybeDropItem();
                var numSouls = (Mathf.FloorToInt(Random.value * 10) / 3) + 1;
                
                for (var i = 0; i < numSouls; i++){
                    Instantiate(soul, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }

            healthBar.value = health/maxHealth;
        }
   }

   private void MaybeDropItem() {
       if(Random.value <= dropChance) {
           var item = GetItemToDrop();
           Instantiate(item, transform.position, Quaternion.identity);
        }
   }

   private GameObject GetItemToDrop() {
       float value = Random.value;

       if (value <= acaiDropChance){
           return acai;
       } 
       else if (value <= guaranaDropChance){
           return guarana;
       }
       else {
           return coco;
       }
   }

    IEnumerator DamageAnimation(){
        
        spriteRenderer.color=new Vector4(255/255f, 0/255f, 0/255f,0.3f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color=Color.white;
    }

    public virtual void BecomeAggresive(){
       
   }

    public virtual void Animate(){

   }
    public virtual void SetProperties(){

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
    public void getAngle(EnemiesStateManager enemy){

        Vector3 fromPosition = enemy.transform.position;
        Vector3 toPosition = enemy.target.transform.position;
        
        Vector3 direction = toPosition - fromPosition;

        direction.Normalize();
        enemy.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        
    }



}
