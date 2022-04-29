using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStateManager : EnemiesStateManager
{

    public WolfAggressiveState aggressiveState = new WolfAggressiveState();

    public override void BecomeAggresive()
    {
        SwitchState(aggressiveState);
    }
}
