using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float castRadius;
    private ShipStats _shipStats;
    private Transform _lastObjectHit;
    private bool _collided;

    private void OnEnable()
    {
        GameEventsManager.removeBullet += DestroyBullet;
        _lastObjectHit = null;
    }

    private void OnDisable()
    {
        GameEventsManager.removeBullet -= DestroyBullet;
    }

    private void OnBecameInvisible()
    {
        DestroyBullet(this);
    }

    private void DestroyBullet(Bullet bullet)
    {
        if(bullet != this) return;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var hit = Physics2D.CircleCast(transform.position, castRadius, Vector2.zero, float.PositiveInfinity);
        if (!hit)
        {
            _collided = false;
            return;
        }
        if(_collided&&hit.transform.Equals(_lastObjectHit)) return;
        _collided = true;
        _lastObjectHit = hit.transform;
        GameEventsManager.BulletHitTrigger(hit.transform, this);
    }

    public void SetDirection(Vector2 direction)
    {
        body.velocity = direction;
    }

    public void SetShipStats(ShipStats shipStats)
    {
        _shipStats = shipStats;
    }
    
    public ShipStats GetShipStats()
    {
        return _shipStats;
    }
}
