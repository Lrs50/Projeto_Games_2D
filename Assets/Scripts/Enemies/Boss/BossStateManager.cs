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
    public GameObject bullet;
    public FeatherProjectile bulletProperties;
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
    void Start()
    {        
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        bulletProperties = bullet.GetComponent<FeatherProjectile>();
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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Acertou o player");
        }
    }

}