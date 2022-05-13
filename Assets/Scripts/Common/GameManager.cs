using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public HubState hubState = new HubState();
    static public MenuState menuState = new MenuState();
    static public PreStartState preStart = new PreStartState();
    static public CreditsStates credits = new CreditsStates(); 
    static public P1_0State p1_0 = new P1_0State(); 
    static public P1_1State p1_1 = new P1_1State(); 
    static public P1_2State p1_2 = new P1_2State();  
    static public P1_EndState p1_Final = new P1_EndState(); 
    static public BaseStateScenes currentState = menuState;
    static public GameManager gameManager;
    
    static public PlayerStateManager player;

    public AudioSource audioSource;
    public AudioClip menuSound;
    public AudioClip bonusSound;
    public AudioClip fasesSound;
    public AudioClip combateSound;
    public AudioClip bossSound;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Start()
    {
        if(gameManager==null){
            gameManager = this;
        }else{            
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        currentState.UpdateState(this);
        if(player!=null){
            if(player.dead){
                SwitchState(hubState);
            }
            if(player.toMenu){
                SwitchState(menuState);
            }
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter(this);
    }


    public void SwitchState(BaseStateScenes next){
        audioSource.volume=0.07f;
        audioSource.Pause();
        currentState = next;
        currentState.EnterState(this);
    }

    public void MenuToHub(){
        currentState = preStart;
        currentState.EnterState(this);
    }

    public void ShowCredits(){
        currentState = credits;
        currentState.EnterState(this);
    }

    public void ResetPlayer(){
        player.health=100;
        player.mana=100;
        player.guaranaQty=0;
        player.jabuticabaQty=0;
        player.animationMode="normal";
        player.setWings(false);
        player.SetAnimationMode();
    }
    public void UpdatePlayer(PlayerStateManager old){
        player.health=old.health;
        player.mana=old.mana;
        player.guaranaQty=old.guaranaQty;
        player.jabuticabaQty=old.jabuticabaQty;
        player.animationMode=old.animationMode;
        player.setWings(player.wings.activeSelf);
        player.SetAnimationMode();
    }

    public void Exit(){
        Application.Quit();
    }

}
