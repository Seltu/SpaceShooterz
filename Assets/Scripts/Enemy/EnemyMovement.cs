using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement<T> : MonoBehaviour where T : CurveEnemyAi 
{
    [SerializeField] protected T ai;

    protected virtual void Awake()
    {
        ai.OnMove.AddListener(Move);
    }

    protected virtual void Start()
    {
    }

    protected virtual void Move()
    {
        throw new NotImplementedException();
    }
}