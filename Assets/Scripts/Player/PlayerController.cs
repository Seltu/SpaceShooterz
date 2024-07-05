using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDeath;
    [HideInInspector] public UnityEvent<Vector2> OnMove;
    [HideInInspector] public UnityEvent OnShoot;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private LevelInfoSO levelInfoSO;
    [SerializeField] private GameEvent shootEvent;
    internal object onDeath;

    private void Awake()
    {
        OnShoot.AddListener(Shoot);
        levelInfoSO.players.Add(this);
    }

    private void Shoot()
    {
        shootEvent.Raise(gameObject);
    }

    public PlayerStats GetStats()
    {
        return playerStats;
    }
}
