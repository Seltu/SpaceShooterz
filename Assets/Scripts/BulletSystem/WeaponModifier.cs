using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class WeaponModifierContainer
{
    [SerializeReference] public List<WeaponModifier> list;
}

[Serializable]
public class WeaponModifier
{
    public virtual ShotInfo Modify(ShotInfo input)
    {
        // Base implementation
        return new ShotInfo();
    }
}

public class BulletAngleModifier : WeaponModifier
{

    [SerializeField] float bulletAngle;
    public override ShotInfo Modify(ShotInfo input)
    {
        var startingAngle = input.weaponStats.BulletAmount <= 1 ? 0 : -bulletAngle * input.weaponStats.BulletAmount / 2 + bulletAngle / 2;
        input.rotationShift += startingAngle + input.bulletIndex * bulletAngle;
        return input;
    }
}

public class BulletArrayModifier : WeaponModifier
{
    [SerializeField] int bulletsPerArray;
    [SerializeField] float arrayAngle;
    public override ShotInfo Modify(ShotInfo input)
    {
        if (bulletsPerArray == 0) return input;
        var startingAngle = input.weaponStats.BulletAmount <= 1 ? 0 : -arrayAngle * (input.weaponStats.BulletAmount / bulletsPerArray)/2 + arrayAngle / 2;
        input.rotationShift += startingAngle + input.bulletIndex/bulletsPerArray * arrayAngle;
        return input;
    }
}

public class BulletWidenessModifier : WeaponModifier
{

    [SerializeField] float bulletWideness;
    public override ShotInfo Modify(ShotInfo input)
    {
        var startingSpread = input.weaponStats.BulletAmount <= 1 ? 0 : -bulletWideness * input.weaponStats.BulletAmount / 2 + bulletWideness / 2;
        input.directionShift += Vector2.right * (startingSpread + input.bulletIndex * bulletWideness);
        return input;
    }
}

public class BulletOffsetModifier : WeaponModifier
{

    [SerializeField] Vector2 bulletOffset;
    public override ShotInfo Modify(ShotInfo input)
    {
        input.position += bulletOffset;
        return input;
    }
}

public class BulletRandomSizeModifier : WeaponModifier
{
    [SerializeField] Vector2 randomSize;
    public override ShotInfo Modify(ShotInfo input)
    {
        input.position += new Vector2(UnityEngine.Random.Range(-randomSize.x, randomSize.x), UnityEngine.Random.Range(-randomSize.y, randomSize.y));
        return input;
    }
}

public class BulletAngleOffsetModifier : WeaponModifier
{

    [SerializeField] float bulletAngleOffset;
    public override ShotInfo Modify(ShotInfo input)
    {
        input.rotationShift += bulletAngleOffset;
        return input;
    }
}

public class BulletTiltModifier : WeaponModifier
{
    [SerializeField] private float bulletTilt;
    [SerializeField] private int maxTime;
    public float BulletTilt { get => bulletTilt; set => bulletTilt = value; }

    public override ShotInfo Modify(ShotInfo input)
    {
        var calculatedTime = input.bulletTime;
        if (maxTime > 0)
        {
            var sign = input.bulletTime % (maxTime*2) > input.bulletTime % maxTime ? -1 : 1;
            calculatedTime = input.bulletTime % maxTime;
            calculatedTime = sign*maxTime/2 -sign*calculatedTime;
        }
        input.rotationShift += calculatedTime * BulletTilt;
        return input;
    }
}

public class BulletSpeedModifier : WeaponModifier
{
    [SerializeField] private float extraSpeed;

    public override ShotInfo Modify(ShotInfo input)
    {
        input.directionShift += Vector2.up * extraSpeed;
        return input;
    }
}