using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStat<TStat> where TStat : struct
{
    #region fields
    private TStat _currentValue;

    private bool _hasMaximumValue;
    private TStat _maximumValue;

    private bool _hasMinimumValue;
    private TStat _minimumValue;

    private bool _hasDefaultValue;
    private TStat _defaultValue;
    #endregion


    #region properties
    public TStat Value { get => _currentValue; set => _currentValue = value; }
    public TStat Max {
        get => _hasMaximumValue ? _maximumValue : default;
        set { _maximumValue = value; _hasMaximumValue = true; }
    }
    public TStat Min
    {
        get => _hasMinimumValue ? _minimumValue : default;
        set { _minimumValue = value; _hasMinimumValue = true; }
    }
    public TStat Default
    {
        get => _hasDefaultValue ? _defaultValue : default;
        set { _defaultValue = value; _hasDefaultValue = true; }
    }
    #endregion


    #region unset
    public ActorStat<TStat> UnsetMaximumValue() { _hasMaximumValue = false; return this; }
    public ActorStat<TStat> UnsetMinumumValue() { _hasMinimumValue = false; return this; }
    public ActorStat<TStat> UnsetDefaultValue() { _hasDefaultValue = false; return this; }
    #endregion


    public ActorStat(TStat initialValue)
    {
        _hasMaximumValue = false;
        _hasMinimumValue = false;
        _hasDefaultValue = false;

        _currentValue = initialValue;
    }
}