using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateManager : EnemiesStateManager
{

    public SheepAggressiveState aggressiveState = new SheepAggressiveState();

    public SheepTackleState tackleState = new SheepTackleState();
   
    public override void BecomeAggresive()
    {
        SwitchState(aggressiveState);
    }
}
