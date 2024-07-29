using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysEnemyMovement : EnemyMovement<EnemyAi>
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Collider2D enemyCollider;
    private LayerMask boundsLayerMask;
    private void Start()
    {
        boundsLayerMask = LayerMask.GetMask("PlayerBarrier");
        rigidBody.velocity = Vector2.right*ai.GetStats().GetMovementSpeed();
    }

    protected override void Move()
    {
        var hit = Physics2D.Raycast(transform.position, rigidBody.velocity.normalized, enemyCollider.bounds.extents.x+0.1f, boundsLayerMask);
        if (hit)
            rigidBody.velocity *= Vector2.left;
    }
}
