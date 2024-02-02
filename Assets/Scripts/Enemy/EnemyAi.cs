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
    private bool _delay;
    protected virtual void Update()
    {
        OnMove.Invoke();
        if (_delay) return;
        Shoot();
    }

    private void Shoot()
    {
        GameEventsManager.ShootBulletTrigger(gameObject);
    }

    public EnemyStats GetStats()
    {
        return enemyStats;
    }

    internal void Eliminate()
    {
        gameObject.SetActive(false);
    }

    public void SetDelay(float delay)
    {
        StartCoroutine(WaitDelay(delay));
    }

    private IEnumerator WaitDelay(float delay)
    {
        if (_delay) yield break;
        _delay = true;
        yield return new WaitForSeconds(delay);
        _delay = false;
    }

}
