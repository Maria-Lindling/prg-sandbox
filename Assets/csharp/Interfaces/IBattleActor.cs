using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleActor<TKey, TStat> where TStat : struct
{
    public bool IsPlayerAligned { get; set; }

    public IStatSheet<TKey, TStat> Stats { get; }
    GameObject GameObject { get; set; }
}
