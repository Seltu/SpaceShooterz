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
    [SerializeField] int bulletsPerAngleChange = 1;
    [SerializeField] float arrayAngle;
    public override ShotInfo Modify(ShotInfo input)
    {
        if (bulletsPerAngleChange == 0) return input;
        var startingAngle = input.weaponStats.BulletAmount <= 1 ? 0 : -arrayAngle * (input.weaponStats.BulletAmount / bulletsPerAngleChange) / 2 + arrayAngle / 2;
        input.rotationShift += startingAngle + input.bulletIndex / bulletsPerAngleChange * arrayAngle;
        return input;
    }
}

public class BulletSeparationModifier : WeaponModifier
{
    [SerializeField] private int bulletsPerSeparation = 1;
    [SerializeField] private Vector2 arrayOffset;

    public override ShotInfo Modify(ShotInfo input)
    {
        if (bulletsPerSeparation == 0) return input;
        var startingPosition = input.weaponStats.BulletAmount <= 1 ? Vector2.zero : -arrayOffset * (input.weaponStats.BulletAmount / bulletsPerSeparation) / 2 + arrayOffset / 2;
        input.position += startingPosition + input.bulletIndex / bulletsPerSeparation * arrayOffset;
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
        var calculatedTime = (float)input.bulletTime;
        if (maxTime > 0)
        {
            float a = (calculatedTime - maxTime) / (2f * maxTime);
            float b = a - Mathf.Floor(a + 0.5f);
            calculatedTime = (2f * maxTime * Mathf.Abs(b) - maxTime/2f);
        }
        input.rotationShift += calculatedTime * bulletTilt;
        return input;
    }
}

public class BulletPositionAlternatorModifier : WeaponModifier
{
    [SerializeField] private Vector2 alternateOffset;
    [SerializeField] private int maxTime;
    public Vector2 AlternateOffset { get => alternateOffset; set => alternateOffset = value; }

    public override ShotInfo Modify(ShotInfo input)
    {
        var calculatedTime = (float)input.bulletTime;
        if (maxTime > 0)
        {
            float a = (calculatedTime - maxTime) / (2f * maxTime);
            float b = a - Mathf.Floor(a + 0.5f);
            calculatedTime = (2f * maxTime * Mathf.Abs(b) - maxTime / 2f);
        }
        input.position += new Vector2(alternateOffset.x * (calculatedTime / maxTime), alternateOffset.y * (calculatedTime / maxTime));
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