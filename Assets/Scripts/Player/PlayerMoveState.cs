using UnityEngine;

public class PlayerMoveState : BaseStatePlayer {

    public override void EnterState(PlayerStateManager player) {
    	player.rb.MovePosition(player.rb.position + player.walkInput * (player.baseSpeed + (player.sprintInput * player.sprintSpeed)) * Time.fixedDeltaTime);
    }

    public override void UpdateState(PlayerStateManager player) {
        if (player.currentInputVector == Vector2.zero){
        	player.SwitchState(player.idleState);
        }
    }

    public override void FixedUpdateState(PlayerStateManager player){
		player.currentInputVector = Vector2.SmoothDamp(player.currentInputVector, player.walkInput, ref player.smoothInputVelocity, player.smoothInputSpeed);
        player.rb.MovePosition(player.rb.position + player.currentInputVector * (player.baseSpeed + (player.sprintInput * player.sprintSpeed)) * Time.fixedDeltaTime);
    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }
   
}