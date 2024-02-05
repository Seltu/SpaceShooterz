using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private ShipStats ship;
    [SerializeField] private float bulletAmount = 1;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float bulletAngle;
    [SerializeField] private float bulletWideness;
    [SerializeField] private float bulletTilt;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 randomSize;
    [SerializeField] private float angleOffset;
    [SerializeField] private bool invertTilt;
    [SerializeField] private float maxTilt;

    public ShipStats Ship { get => ship; set => ship = value; }
    public float BulletAmount { get => bulletAmount; set => bulletAmount = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float BulletAngle { get => bulletAngle; set => bulletAngle = value; }
    public float BulletTilt { get => bulletTilt; set => bulletTilt = value; }
    public Vector2 Offset { get => offset; set => offset = value; }
    public Vector2 RandomSize { get => randomSize; set => randomSize = value; }
    public float AngleOffset { get => angleOffset; set => angleOffset = value; }
    public bool InvertTilt { get => invertTilt; set => invertTilt = value; }
    public float MaxTilt { get => maxTilt; set => maxTilt = value; }
    public float BulletWideness { get => bulletWideness; set => bulletWideness = value; }
}
