using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
public enum EnemyType { EnemyRed, EnemyBlue, EnemyGreen, EnemyPink }
[Serializable]
public class Wave
{
    public EnemyType enemy;
    public int number;
    public int curve;
    public int layer;
    public float speedMultiplier = 1f;
    public Vector2 offset;
}
