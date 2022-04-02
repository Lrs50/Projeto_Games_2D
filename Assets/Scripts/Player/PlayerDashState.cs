using UnityEngine;
using System.Collections;
public class PlayerDashState: BaseStatePlayer {
    public override void EnterState(PlayerStateManager player){

        player.StartCoroutine(Dash(player));
        
    }

    public override void UpdateState(PlayerStateManager player){

    }

    public override void FixedUpdateState(PlayerStateManager player){

    }

    public override void OnCollisionEnter(PlayerStateManager player){

    }
    
    private IEnumerator Dash(PlayerStateManager player){
        Vector2 direction = player.walkInput;
        player.rb.AddForce(direction*player.dashMag,ForceMode2D.Impulse);

        yield return new WaitForSeconds(player.dashTimer);
        player.SwitchState(player.moveState);
    } 

}
