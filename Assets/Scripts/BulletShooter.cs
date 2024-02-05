using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private WeaponStats _weaponStats;
    [SerializeField] private Bullet bulletPrefab;
    private float _cooldown;
    private float _bulletTilt;
    private bool _invertedTilt;
    
    private void OnEnable()
    {
        GameEventsManager.shootBullet += ShootPattern;
    }

    private void OnDisable()
    {
        GameEventsManager.shootBullet -= ShootPattern;
    }

    private void Update()
    {
        if(_cooldown > 0)
            _cooldown -= Time.deltaTime;
    }

    private void ShootPattern(GameObject ship)
    {
        if(ship != _weaponStats.Ship.gameObject) return;
        if(_cooldown > 0) return;
        _cooldown = 1f / _weaponStats.FireRate;
        var startingAngle = _weaponStats.BulletAmount <= 1 ? 0 : -_weaponStats.BulletAngle * _weaponStats.BulletAmount / 2 + _weaponStats.BulletAngle / 2;
        var startingSpread = _weaponStats.BulletAmount <= 1 ? 0 : -_weaponStats.BulletWideness * _weaponStats.BulletAmount / 2 + _weaponStats.BulletWideness / 2;
        for (var i = 0; i < _weaponStats.BulletAmount; i++)
        {
            Shoot(startingAngle+i* _weaponStats.BulletAngle, startingSpread + i * _weaponStats.BulletWideness);
        }
        _bulletTilt += _invertedTilt ? _weaponStats.BulletTilt: -_weaponStats.BulletTilt;
        if(_weaponStats.InvertTilt)
        {
            if (Mathf.Abs(_bulletTilt) > _weaponStats.MaxTilt)
            {
                _invertedTilt = !_invertedTilt;
            }
        }
    }

    private void Shoot(float angle, float spread)
    {
        var transform1 = transform;
        var position = (Vector2)transform1.position + _weaponStats.Offset + new Vector2(Random.Range(-_weaponStats.RandomSize.x, _weaponStats.RandomSize.x),
                                                                                        Random.Range(-_weaponStats.RandomSize.y, _weaponStats.RandomSize.y));
        var rotation = transform1.rotation * Quaternion.AngleAxis(_weaponStats.AngleOffset + angle + _bulletTilt, Vector3.forward);
        var bullet = _weaponStats.Ship.GetBulletPool().Create(bulletPrefab, position, rotation);
        bullet.SetDirection((Vector2)bullet.transform.up * _weaponStats.ProjectileSpeed + Vector2.Perpendicular(bullet.transform.up)*spread);
        bullet.SetShipStats(_weaponStats.Ship);
    }
}
