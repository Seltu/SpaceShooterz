using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDeath;
    [HideInInspector] public UnityEvent<Vector2> OnMove;
    [HideInInspector] public UnityEvent OnShoot;
    [SerializeField] private PlayerStats playerStats;
    internal object onDeath;

    private void Awake()
    {
        OnShoot.AddListener(Shoot);
    }

    private void Shoot()
    {
        GameEventsManager.ShootBulletTrigger(gameObject);
    }

    public PlayerStats GetStats()
    {
        return playerStats;
    }
}
