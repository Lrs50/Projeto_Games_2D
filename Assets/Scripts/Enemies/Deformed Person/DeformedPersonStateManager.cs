using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedPersonStateManager : EnemiesStateManager
{
    public DeformedPersonAggressiveState aggresiveState {get; protected set;} = new DeformedPersonAggressiveState();

    public override void BecomeAggresive()
    {
        SwitchState(aggresiveState);
    }
}
