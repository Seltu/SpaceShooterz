using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertTiltOnRight : MonoBehaviour
{
    [SerializeField] private WeaponStats weaponStats;

    private void Start()
    {
        if (transform.position.x > 0)
            weaponStats.BulletTilt = -weaponStats.BulletTilt;
    }
}
