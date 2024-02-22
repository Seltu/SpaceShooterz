using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private ShipStats ship;
    [SerializeField] private int bulletAmount = 1;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [BF_SubclassList.SubclassList(typeof(WeaponModifier))] [SerializeField] private WeaponModifierContainer modifiers;

    public ShipStats Ship { get => ship; set => ship = value; }
    public int BulletAmount { get => bulletAmount; set => bulletAmount = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public List<WeaponModifier> Modifiers { get => modifiers.list; set => modifiers.list = value; }
}
