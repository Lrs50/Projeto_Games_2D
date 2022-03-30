using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState{
   
    public override void EnterState(PlayerStateManager player) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (player.walkInput != Vector2.zero){
            player.SwitchState(player.moveState);
        }
    }

    public override void FixedUpdateState(PlayerStateManager player) {

    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

}
