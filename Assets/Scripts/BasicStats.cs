using System;
using UnityEngine;

public class BasicStats : MonoBehaviour
{
    // Entidades da mesma equipe não se machucam
    [SerializeField] protected internal int team;
    [SerializeField] private float maxHp;
    [SerializeField] protected GameEventListener<CustomEvent<object, Bullet>> bulletHitEvent;
    [SerializeField] private GameEvent removeBulletEvent;
    private static int lastId;
    private int id;
    protected float CurrentHp;

    private void Start()
    {
        bulletHitEvent.AddListener<object, Bullet>(CheckHit);
        ResetStats();
    }

    private void OnDestroy()
    {
        bulletHitEvent.RemoveListener<object, Bullet>(CheckHit);
    }

    protected virtual void ResetStats()
    {
        id = lastId++;
        CurrentHp = maxHp;
    }

    protected void CheckHit(object hit, Bullet bullet)
    {
        if (!((object)transform == hit)) return;
        if(bullet.GetShipStats().team == team) return;
        TakeHit(bullet);
        removeBulletEvent.Raise(bullet);
    }

    protected virtual void TakeHit(Bullet bullet)
    {
        TakeDamage(bullet.GetShipStats().GetDamage());
    }

    protected virtual bool TakeDamage(float damage)
    {
        CurrentHp-=damage;
        if (CurrentHp < 0)
        {
            // Futuramente usar entity pooling para o desligamento de entidades
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
    
    protected void Heal(float health)
    {
        CurrentHp+=health;
        if (CurrentHp > maxHp)
        {
            CurrentHp = maxHp;
        }
    }

    public int GetId()
    {
        return id;
    }
}
