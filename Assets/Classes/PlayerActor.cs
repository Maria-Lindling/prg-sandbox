using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActor : Actor, IActor
{

    

    public PlayerActor() : base()
    {

    }


    public override IBattleActor GenerateBattleActor()
    {
        return new BattleActor(this);
    }
}
