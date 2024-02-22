using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<Bullet> bulletPool = new List<Bullet>();

    public Bullet Create(Bullet bulletPrefab, Vector2 position, Quaternion rotation)
    {
        foreach (Bullet bullet in bulletPool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.transform.SetPositionAndRotation(position, rotation);
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        var newBullet = Instantiate(bulletPrefab, position, rotation);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void ClearPool()
    {
        foreach (Bullet bullet in bulletPool)
        {
            Destroy(bullet.gameObject);
        }
        bulletPool.Clear();
    }
}
