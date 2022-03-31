using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{

    [SerializeField]
    public float baseSpeed = 15f;

    [SerializeField]
    public float sprintSpeed = 15f;

    [SerializeField]
    public float dashSpeed = 5f;

    [SerializeField]
    public float smoothInputSpeed = .05f;

    BaseState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();

    public Rigidbody2D rb;

    public Vector2 walkInput;
    public float sprintInput;
    public float dashInput;

    public Vector2 currentInputVector;
    public Vector2 smoothInputVelocity;

    // Start is called before the first frame update
    void Start() {
        currentState = idleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update() {
        currentState.UpdateState(this);
    }

    void FixedUpdate() {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(BaseState state){
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnWalk(InputAction.CallbackContext context) {
        walkInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        sprintInput = context.ReadValue<float>();
    }

    /* TODO: Player dash state
    public void OnDash(InputAction.CallbackContext context) {
        dashInput = context.ReadValue<float>();
    }
    */
}
