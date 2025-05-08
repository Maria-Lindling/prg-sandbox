using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint
{

    private Transform _transform;
    public Transform Transform { get => _transform; set => _transform = value; }

    private Actor _actor;
    public Actor Actor { get => _actor; set => _actor = value; }

    private bool _blocked;
    public bool Blocked { get => _blocked; set => _blocked = value; }


    private bool _becomesBlocked;

    public SpawnPoint(Transform transform, bool becomesBlocked)
	{
        _transform  = transform;
        _actor      = null;
        _blocked    = false;

        _becomesBlocked = becomesBlocked;
    }

    public Transform Use()
    {
        _blocked = _becomesBlocked || _blocked;
        return _transform;
    }
}
