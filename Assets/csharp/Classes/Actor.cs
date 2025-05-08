using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : IActor
{
    protected string _name;
    public string Name => _name;

    public Actor(string name)
    {
        _name = name;
    }


    virtual public BattleActor GenerateBattleActor()
    {
        return new(Name,false);
    }
}
