using System;
using UnityEngine;

public class PlayerStats : ShipStats
{
    [SerializeField] private float invincibilityTime;
    [SerializeField] private GameEvent playerDeathEvent;
    private float _hpMultiplier = 1;
    private float _damageMultiplier = 1;
    private float _fireRateMultiplier = 1;

    public override float GetDamage()
    {
        return base.GetDamage() * _damageMultiplier;
    }

    protected override void TakeHit(Bullet bullet)
    {
        var shooter = bullet.GetShipStats();
        if(TakeDamage(shooter.GetDamage()));
            playerDeathEvent.Raise(this, shooter);
    }
    
}
