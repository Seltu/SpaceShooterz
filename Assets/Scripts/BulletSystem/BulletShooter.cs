using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private WeaponStats _weaponStats;
    [SerializeField] private Bullet bulletPrefab;
    private float _cooldown;
    private int _bulletTime;
    
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
        for (var i = 0; i < _weaponStats.BulletAmount; i++)
        {
            var shotInfo = new ShotInfo(_weaponStats, i, _bulletTime, transform.position, 0, Vector2.zero);
            Shoot(shotInfo);
        }
        _bulletTime += 1;
    }

    private void Shoot(ShotInfo shotInfo)
    {
        foreach(WeaponModifier modifier in _weaponStats.Modifiers)
        {
            shotInfo = modifier.Modify(shotInfo);
        }
        var rotation = transform.rotation * Quaternion.AngleAxis(shotInfo.rotationShift, Vector3.forward);
        var bullet = _weaponStats.Ship.GetBulletPool().Create(bulletPrefab, shotInfo.position, rotation);
        bullet.SetDirection(bullet.transform.up * _weaponStats.ProjectileSpeed + bullet.transform.TransformDirection(shotInfo.directionShift));
        bullet.SetShipStats(_weaponStats.Ship);
    }
}
