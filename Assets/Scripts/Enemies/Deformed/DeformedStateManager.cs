using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedStateManager : EnemiesStateManager
{

    DeformedAggressiveState aggressiveState = new DeformedAggressiveState();

    public override void BecomeAggresive()
    {
        SwitchState(aggressiveState);
    }

}
