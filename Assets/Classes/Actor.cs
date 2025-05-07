using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : IActor
{
    public Actor()
    {

    }


    virtual public IBattleActor GenerateBattleActor()
    {
        return new BattleActor(this);
    }
}
