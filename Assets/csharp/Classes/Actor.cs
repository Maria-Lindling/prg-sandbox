using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    protected string _name;
    public string Name => _name;

    public Actor(string name)
    {
        _name = name;
    }
}
