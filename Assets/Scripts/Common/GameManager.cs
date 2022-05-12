using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public HubState hubState = new HubState();
    static public MenuState menuState = new MenuState();
    static public PreStartState preStart = new PreStartState();
    //static public CreditsStates 
    static public BaseStateScenes currentState = menuState;
    static public GameManager gameManager;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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
        currentState = next;
        currentState.EnterState(this);
    }

    public void MenuToHub(){
        currentState = hubState;
        currentState.EnterState(this);
    }

    public void ShowCredits(){
        //currentState = CreditsStates;
        currentState.EnterState(this);
    }

}
