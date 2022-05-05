using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerEvolveState : BaseStatePlayer{

    public override void EnterState(PlayerStateManager player) {
        player.StartEvolve();
    }

    public override void UpdateState(PlayerStateManager player) {

    }


    public override void FixedUpdateState(PlayerStateManager player) {

    }

    public override void OnCollisionEnter(PlayerStateManager player) {

    }

}
