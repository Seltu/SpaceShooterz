using System;
using Unity.VisualScripting;
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
    private float _cooldown;

    private void Update()
    {
        if (_cooldown > 0)
            _cooldown -= Time.deltaTime;
    }

    private void Awake()
    {
        OnShoot.AddListener(Shoot);
        levelInfoSO.players.Add(this);
    }

    private void Shoot()
    {
        if (_cooldown > 0) return;
        shootEvent.Raise(gameObject);
        _cooldown = 1f / playerStats.GetWeapon().FireRate;
    }

    public PlayerStats GetStats()
    {
        return playerStats;
    }
}
