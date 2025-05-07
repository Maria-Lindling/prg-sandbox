using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{

    public IBattleActor<string, int> GenerateBattleActor();
}
