using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement<T> : MonoBehaviour where T : EnemyAi
{
    [SerializeField] protected T ai;

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        throw new NotImplementedException();
    }
}
