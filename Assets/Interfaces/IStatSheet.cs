using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatSheet<TKey,TStat> where TStat : struct
{
    public Dictionary<TKey,ActorStat<TStat>> All { get; }
}
