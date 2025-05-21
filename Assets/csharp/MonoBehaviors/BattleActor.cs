using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleActor : MonoBehaviour
{
    // An instance of the ScriptableObject defined above.
    public BattleActorScriptableObject battleActorValues;


    public string Name { get => battleActorValues.prefabName; private set => battleActorValues.prefabName = value; }
    public bool IsPlayerControlled { get; set; }

    public bool IsAlive => _isAlive;

    private bool _isAlive = true;


    #region static
    public static List<GameObject> Instances;

    static BattleActor()
    {
        Instances = new();
    }
    #endregion
}
