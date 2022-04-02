using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseStatePlayer{
   
    public override void EnterState(PlayerStateManager player) {
        
    }

    public override void UpdateState(PlayerStateManager player) {
        if (player.walkInput != Vector2.zero){
            player.SwitchState(player.moveState);
            return;
        }
    }

    public override void FixedUpdateState(PlayerStateManager player) {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

}
