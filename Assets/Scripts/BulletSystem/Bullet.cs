using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float castRadius;
    [SerializeField] private GameEventListener<CustomEvent<object>> removeBullet;
    [SerializeField] private GameEvent bulletHit;
    private ShipStats _shipStats;
    private Transform _lastObjectHit;
    private bool _collided;

    private void Start()
    {
        removeBullet.AddListener<object>(DestroyBullet);
    }

    private void OnDestroy()
    {
        removeBullet.RemoveListener<object>(DestroyBullet);
    }

    private void OnBecameInvisible()
    {
        DestroyBullet(this);
    }

    private void DestroyBullet(object bullet)
    {
        if(bullet != (object)this) return;
        _lastObjectHit = null;
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
        bulletHit.Raise(hit.transform, this);
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
