using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvertTiltOnRight : MonoBehaviour
{
    [SerializeField] private WeaponStats weaponStats;

    private void Start()
    {
        if (transform.position.x > 0) {
            var tilt = weaponStats.Modifiers.Find(sh => sh is BulletTiltModifier) as BulletTiltModifier;
            tilt.BulletTilt = -tilt.BulletTilt;
        }
    }
}
