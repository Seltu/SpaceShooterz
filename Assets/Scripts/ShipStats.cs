using UnityEngine;

public class ShipStats : BasicStats
{
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed;

    public virtual float GetDamage()
    {
        return damage;
    }
    
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}
