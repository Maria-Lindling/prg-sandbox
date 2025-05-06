using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEncounter
{
    private readonly List<string> _participants;
    public IEnumerable<string> Enemies => _participants.Where((actor) => { return actor == "enemy"; });
    public IEnumerable<string> Allies => _participants.Where((actor) => { return actor  == "ally"; });


    public CombatEncounter()
    {
        _participants = new();

        BaseCharacterController.Instance.InputLock = true;
    }


    void EndEncounter()
    {
        BaseCharacterController.Instance.InputLock = false;
    }
}
