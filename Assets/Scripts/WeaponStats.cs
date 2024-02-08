using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private ShipStats ship;
    [SerializeField] private float bulletAmount = 1;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool invertTime;
    [SerializeField] private float maxTime;
    [BF_SubclassList.SubclassList(typeof(WeaponModifier))] [SerializeField] private WeaponModifierContainer modifiers;

    public ShipStats Ship { get => ship; set => ship = value; }
    public float BulletAmount { get => bulletAmount; set => bulletAmount = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public bool InvertTime { get => invertTime; set => invertTime = value; }
    public float MaxTime { get => maxTime; set => maxTime = value; }
    public List<WeaponModifier> Modifiers { get => modifiers.list; set => modifiers.list = value; }
}
