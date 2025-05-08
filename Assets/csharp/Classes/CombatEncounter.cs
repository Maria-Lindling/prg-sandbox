using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEncounter
{
    private readonly List<BattleActor> _participants;
    public IEnumerable<BattleActor> Allies => _participants.Where((battleActor) => battleActor.IsPlayerControlled);
    public IEnumerable<BattleActor> Enemies => _participants.Where((battleActor) => !battleActor.IsPlayerControlled);


    public CombatEncounter()
    {
        _participants = new();

        BaseCharacterController.Instance.InputLock = true;
    }

    public void LoadCharacters()
    {
        PlayerActor.CurrentParty.ForEach( a => _participants.Append(a.GenerateBattleActor()) );
    }

    public void LoadEnemies(EncounterTables encounterTable = EncounterTables.Default)
    {
        switch (encounterTable)
        {
            case EncounterTables.Default:
                for (int i = 0; i < 3; i++)
                    _participants.Append(
                        MonsterManager
                        .Instance
                        .MonsterManual
                        .First( a => a.Name == "Slug")
                        .GenerateBattleActor()
                ) ;
                break;
        }
    }

    public void EndEncounter()
    {
        BaseCharacterController.Instance.InputLock = false;
    }
}
