using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShotInfo
{
    public WeaponStats weaponStats;
    public int bulletIndex;
    public int bulletTime;
    public Vector2 position;
    public float rotationShift;
    public Vector2 directionShift;

    public ShotInfo(WeaponStats weaponStats, int bulletIndex, int bulletTime, Vector2 position, float rotationShift, Vector2 direction)
    {
        this.weaponStats = weaponStats;
        this.bulletIndex = bulletIndex;
        this.bulletTime = bulletTime;
        this.position = position;
        this.rotationShift = rotationShift;
        this.directionShift = direction;
    }
}
