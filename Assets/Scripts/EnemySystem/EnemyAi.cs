using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAi : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector2> OnDeath;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameEvent shootEvent;
    private bool _delay;
    private float _delayTimer;
    private EnemyPoint _boxPoint;
    private bool _hasBox;

    private void Update()
    {
        if (!_delay) return;
        if (_delayTimer > 0)
            _delayTimer -= Time.deltaTime;
        else
        {
            enemyStats.SetShooting(true);
            _delay = false;
        }
    }

    private void Shoot()
    {
        shootEvent.Raise(gameObject);
    }

    public EnemyStats GetStats()
    {
        return enemyStats;
    }

    public EnemyPoint GetBoxPoint()
    {
        return _boxPoint;
    }

    public bool HasBox()
    {
        return _hasBox;
    }

    internal void Eliminate()
    {
        if(_boxPoint != null)
        {
            Shoot();
            _boxPoint.onRelease -= ReleaseBoxPoint;
            _boxPoint.Taken = false;
            _boxPoint = null;
            _hasBox = false;
        }
        gameObject.SetActive(false);
    }

    public void SetDelay(float delay)
    {
        _delay = true;
        _delayTimer = delay;
        enemyStats.SetShooting(false);
    }

    public void SetBoxPoint(EnemyPoint boxPoint)
    {
        if(_boxPoint != null)
            _boxPoint.onRelease -= ReleaseBoxPoint;
        _boxPoint = boxPoint;
        _hasBox = true;
        _boxPoint.onRelease += ReleaseBoxPoint;
    }

    private void ReleaseBoxPoint()
    {
        _boxPoint = null;
        _hasBox = false;
    }

}
