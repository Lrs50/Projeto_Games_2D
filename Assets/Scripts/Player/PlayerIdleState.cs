using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : BaseStatePlayer{
   float delay=1f/2f;
    public override void EnterState(PlayerStateManager player) {
        player.numFrames = 2;
        player.numFrames= (int)(player.numFrames/delay);

    }

    public override void UpdateState(PlayerStateManager player) {
        if (player.walkInput != Vector2.zero){
            player.SwitchState(player.moveState);
            return;
        }
    }


    public override void FixedUpdateState(PlayerStateManager player) {
        player.spriteRenderer.sprite = player.idleAnimation[player.animationOrientation + (int)((float)player.animationFrame*delay)*4];
    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

}
