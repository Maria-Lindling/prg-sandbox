using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActor : Actor, IActor
{
    private static List<PlayerActor> _currentParty;
    public static List<PlayerActor> CurrentParty => _currentParty;

    static PlayerActor()
    {
        _currentParty = new();
    }

    public PlayerActor(string name) : base(name)
    {

    }


    public override BattleActor GenerateBattleActor()
    {
        return new(Name,true);
    }
}
