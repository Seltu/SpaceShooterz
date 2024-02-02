using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static event PlayerDeath playerDeath;
    public delegate void PlayerDeath(PlayerStats player, ShipStats killer);
    public static event ShootBullet shootBullet;
    public delegate void ShootBullet(GameObject ship);
    public static event BulletHit bulletHit;
    public delegate void BulletHit(Transform hit, Bullet shooter);
    public static event RemoveBullet removeBullet;
    public delegate void RemoveBullet(Bullet bullet);
    public static void ShootBulletTrigger(GameObject ship)
    {
        shootBullet?.Invoke(ship);
    }
    
    public static void BulletHitTrigger(Transform hit, Bullet shooter)
    {
        bulletHit?.Invoke(hit, shooter);
    }

    public static void PlayerDeathTrigger(PlayerStats player, ShipStats killer)
    {
        playerDeath?.Invoke(player, killer);
    }

    public static void RemoveBulletTrigger(Bullet bullet)
    {
        removeBullet?.Invoke(bullet);
    }
}
