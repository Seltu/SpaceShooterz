using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BossStep
{
    public List<GameObject> stepShooters;
    public Round stepSummons;
    public float stepDuration;
}
