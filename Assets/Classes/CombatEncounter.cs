using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEncounter
{
    private readonly List<IBattleActor> _participants;
    public IEnumerable<IBattleActor> Enemies => _participants.Where((battleActor) => !battleActor.IsPlayerAligned);
    public IEnumerable<IBattleActor> Allies => _participants.Where((battleActor) => battleActor.IsPlayerAligned);


    public CombatEncounter()
    {
        _participants = new();

        BaseCharacterController.Instance.InputLock = true;
    }


    public void EndEncounter()
    {
        BaseCharacterController.Instance.InputLock = false;
    }
}
