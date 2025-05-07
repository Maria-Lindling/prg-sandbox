using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleActor : IBattleActor<string,int>
{
    private Actor _representsActor;


    private bool _isPlayerAligned;


    #region battle actor interface
    public bool IsPlayerAligned { get => _isPlayerAligned; set => _isPlayerAligned = value; }
    public IStatSheet<string, int> Stats { get; }
    #endregion


    public BattleActor(Actor representsActor)
    {
        _isPlayerAligned = false;

        _representsActor = representsActor;
    }

}
