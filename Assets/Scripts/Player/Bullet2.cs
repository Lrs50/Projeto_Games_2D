using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : Bullet
{
    override public void SetStats(){
        speed = 10f;
        damage = 10;
        health = 3;
        transform.localScale*=2;
    }

    override public void CollisionAction(){
        transform.localScale/=1.5f;
        damage /=2;
    }

}
