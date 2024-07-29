using System;
using UnityEngine;

public class ShipStats : BasicStats
{
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed;
    private bool _shooting;
    public virtual float GetDamage()
    {
        return damage;
    }
    
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    internal void SetShooting(bool value)
    {
        _shooting = value;
    }

    internal bool GetShooting()
    {
        return _shooting;
    }
}
