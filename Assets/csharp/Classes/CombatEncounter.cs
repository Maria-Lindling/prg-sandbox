using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEncounter
{
    private readonly List<BattleActor> _participants;
    public IEnumerable<BattleActor> Allies => _participants.Where((battleActor) => battleActor.IsPlayerControlled);
    public IEnumerable<BattleActor> Enemies => _participants.Where((battleActor) => !battleActor.IsPlayerControlled);

    public bool PlayerDefeated => !Allies.Any((battleActor) => battleActor.IsAlive);
    public bool EnemiesDefeated => !Enemies.Any((battleActor) => battleActor.IsAlive);
    public bool EndConditionsMet => (PlayerDefeated || EnemiesDefeated);

    public CombatEncounter()
    {
        _participants = new();

        BaseCharacterController.Instance.InputLock = true;
    }

    public void LoadCharacters()
    {
        FightManager.Instance.AllyPool.ForEach( a => _participants.Add(a.GenerateBattleActor(true)) );
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
                        .First( a => a.prefabName == "Slug")
                        .GenerateBattleActor(false)
                ) ;
                break;
        }
    }

    public void EndEncounter()
    {
        // remove all?
        //_participants.ForEach((a) => a);
        BaseCharacterController.Instance.InputLock = false;
    }

}
