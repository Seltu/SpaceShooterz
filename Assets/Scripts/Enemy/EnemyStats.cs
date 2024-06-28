using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : ShipStats
{
    [SerializeField] private EnemyAi ai;
    protected override bool TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0)
        {
            // Futuramente usar entity pooling para o desligamento de entidades
            GameEventsManager.bulletHit -= CheckHit;
            ai.Eliminate();
            return true;
        }
        return false;
    }
}
