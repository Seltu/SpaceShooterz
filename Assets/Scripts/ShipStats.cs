using UnityEngine;

public class ShipStats : BasicStats
{
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private WeaponStats weapon;

    public virtual float GetDamage()
    {
        return damage;
    }
    
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public WeaponStats GetWeapon()
    {
        return weapon;
    }
}
