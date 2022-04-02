using UnityEngine;

public class PlayerMoveState : BaseStatePlayer {

    public override void EnterState(PlayerStateManager player) {
    	//player.rb.MovePosition(player.rb.position + player.walkInput * (player.baseSpeed + (player.sprintInput * player.sprintSpeed)) * Time.fixedDeltaTime);
    }

    public override void UpdateState(PlayerStateManager player) {
        if(player.dashInput!=0f){
            player.SwitchState(player.dashState);
            return;
        }

    }

    public override void FixedUpdateState(PlayerStateManager player){

    
        Vector2 direction = player.walkInput;
        

        if (direction == Vector2.zero){
            ExitState(player);
        	player.SwitchState(player.idleState);
            return;
        }

        

        player.rb.AddForce(direction*player.baseSpeed*(1+player.sprintInput));

        player.rb.velocity = Vector2.ClampMagnitude(player.rb.velocity,player.maxSpeed*(1+player.sprintInput*player.sprintSpeed));
        //Debug.Log(player.rb.velocity);

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,direction);
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation,toRotation,player.rotationSpeed*player.sprintSpeed);
		
    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

    public void ExitState(PlayerStateManager player){
        player.rb.velocity = Vector2.zero;
        player.rb.angularVelocity = 0f;
    }
   
}