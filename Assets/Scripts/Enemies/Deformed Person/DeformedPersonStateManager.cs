using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class DeformedPersonStateManager : EnemiesStateManager
{
    public DeformedPersonAggressiveState aggresiveState {get; protected set;} = new DeformedPersonAggressiveState();
    public Sprite[] fowardSprite;
    public Sprite[] backSprite;
    public Sprite[] leftSprite;
    public Sprite[] rightSprite;

    public override void BecomeAggresive()
    {
        SwitchState(aggresiveState);
    }

}
