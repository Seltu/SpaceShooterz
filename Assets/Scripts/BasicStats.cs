using System;
using UnityEngine;

public class BasicStats : MonoBehaviour
{
    // Entidades da mesma equipe não se machucam
    [SerializeField] protected internal int team;
    [SerializeField] private float maxHp;
    protected float CurrentHp;

    private void OnEnable()
    {
        GameEventsManager.bulletHit += CheckHit;
        ResetStats();
    }

    private void OnDisable()
    {
        GameEventsManager.bulletHit -= CheckHit;
    }

    protected virtual void ResetStats()
    {
        CurrentHp = maxHp;
    }

    private void CheckHit(Transform hit, Bullet bullet)
    {
        if(!transform.Equals(hit)) return;
        if(bullet.GetShipStats().team == team) return;
        TakeHit(bullet);
        GameEventsManager.RemoveBulletTrigger(bullet);
    }

    protected virtual void TakeHit(Bullet bullet)
    {
        TakeDamage(bullet.GetShipStats().GetDamage());
    }

    protected bool TakeDamage(float damage)
    {
        CurrentHp-=damage;
        if (CurrentHp < 0)
        {
            // Futuramente usar entity pooling para o desligamento de entidades
            GameEventsManager.bulletHit -= CheckHit;
            Destroy(gameObject);
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
}
