using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private ShipStats _shipStats;
    [SerializeField] private Bullet bulletPrefab;
    private WeaponStats WeaponStats => _shipStats.GetWeaponStats();
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
        if(ship != _shipStats.gameObject) return;
        if(_cooldown > 0) return;
        _cooldown = 1f / WeaponStats.fireRate;
        var startingAngle = WeaponStats.bulletAmount <= 1 ? 0 : -WeaponStats.bulletAngle * WeaponStats.bulletAmount / 2 + WeaponStats.bulletAngle / 2;
        for(var i = 0; i < WeaponStats.bulletAmount; i++)
        {
            Shoot(startingAngle+i*WeaponStats.bulletAngle);
        }
        _bulletTilt += _invertedTilt ? WeaponStats.bulletTilt: -WeaponStats.bulletTilt;
        if(WeaponStats.invertTilt)
        {
            if (Mathf.Abs(_bulletTilt) > WeaponStats.maxTilt)
            {
                _invertedTilt = !_invertedTilt;
            }
        }
    }

    private void Shoot(float angle)
    {
        var transform1 = transform;
        var position = (Vector2)transform1.position + WeaponStats.offset;
        var rotation = transform1.rotation * Quaternion.AngleAxis(WeaponStats.angleOffset + angle + _bulletTilt, Vector3.forward);
        var bullet = _shipStats.GetBulletPool().Create(bulletPrefab, position, rotation);
        bullet.SetDirection(bullet.transform.up * WeaponStats.projectileSpeed);
        bullet.SetShipStats(_shipStats);
    }
}
