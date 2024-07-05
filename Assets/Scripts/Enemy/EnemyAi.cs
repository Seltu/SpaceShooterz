using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAi : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMove;
    [HideInInspector] public UnityEvent<Vector2> OnDeath;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameEvent shootEvent;
    private EnemyPoint _boxPoint;
    private bool _delay;
    private bool _hasBox;
    private bool _stopShooting;
    protected virtual void Update()
    {
        OnMove.Invoke();
        if (_stopShooting) return;
        if (_delay) return;
        Shoot();
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
        StartCoroutine(WaitDelay(delay));
    }

    public void SetStopShooting(bool stopShooting)
    {
        _stopShooting = stopShooting;
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
        _stopShooting = false;
    }

    private IEnumerator WaitDelay(float delay)
    {
        if (_delay) yield break;
        _delay = true;
        yield return new WaitForSeconds(delay);
        _delay = false;
    }

}
