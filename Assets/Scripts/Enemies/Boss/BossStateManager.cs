using UnityEngine;
using UnityEngine.AI;
public class BossStateManager : MonoBehaviour{
    public Transform target;
    BaseStateBoss currentState;
    public BossSearchState searchState = new BossSearchState();
    public BossAttackState attackState = new BossAttackState();
    public BossFlyState flyingState = new BossFlyState();
    public BossLandingState landingState = new BossLandingState();
    public BossDashAttackState dashAttack = new BossDashAttackState();
    public BossMoveState moveState = new BossMoveState();
    public BossTrackingAttackState trackAttack = new BossTrackingAttackState();
    public BossNormalAttackState normalAttack = new BossNormalAttackState();
    public BossDrillAttackState drillAttack = new BossDrillAttackState();
    public GameObject bullet;
    public GameObject trackingBullet;
    public GameObject normalBullet;
    public GameObject instanceOfTrackingBullet;
    public GameObject instanceOfNormalBullet;
    public FeatherProjectile bulletProperties;
    public TrackingProjectile trackingBulletProperties;
    public NormalProjectile normalBulletProperties;
    public float baseSpeed = 5f;
    public SpriteRenderer spriteRenderer;
    public Sprite shadowSprite;
    public Sprite bossSprite;
    public float startFollowingTime;
    public float followingTime = 5f;
    public Rigidbody2D rb;
    public Collider2D cd;
    public float dashMag;
    public float dashTimer;
    public float maxRange = 5;
    public NavMeshAgent agent;
    public float timerForAttacks;
    public float flySpeed;
    public float searchSpeed;
    public float startDelayDashAttack;
    public float delayToDashAttack;
    public BossShadow shadow;
    public GameObject instanceOfShadow;
    public int qtDash;

    // Sprites

    



    void Start()
    {        
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        bulletProperties = bullet.GetComponent<FeatherProjectile>();
        shadow = instanceOfShadow.GetComponent<BossShadow>();
        //shadow = transform.GetChild(0).gameObject.GetComponent<BossShadow>();
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
                Debug.Log("Acertou o player");
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

    

}