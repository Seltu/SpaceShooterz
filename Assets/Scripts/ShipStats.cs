using UnityEngine;

public class ShipStats : BasicStats
{
    [SerializeField] private float damage;
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private float movementSpeed;
    [SerializeField] private BulletPool bulletPool;

    public virtual float GetDamage()
    {
        return damage;
    }
    
    public virtual WeaponStats GetWeaponStats()
    {
        return weaponStats;
    }
    
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public BulletPool GetBulletPool()
    {
        return bulletPool;
    }
}
