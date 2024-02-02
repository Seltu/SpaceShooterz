using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public float bulletAmount = 1;
    public float fireRate;
    public float projectileSpeed;
    public float bulletAngle;
    public float bulletTilt;
    public Vector2 offset;
    public float angleOffset;
    public bool invertTilt;
    public float maxTilt;
}
