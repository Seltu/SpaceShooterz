using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
[Serializable]
public struct Wave
{
    public int enemy;
    public int number;
    public int curve;
    public int layer;
    public Vector2 offset;

    public Wave(int enemy, int number, int curve, int layer)
    {
        this.enemy = enemy;
        this.number = number;
        this.curve = curve;
        this.layer = layer;
        offset = new Vector2();
    }
    
    public Wave(int enemy, int number, int curve, int offsetX, int offsetY, int layer)
    {
        this.enemy = enemy;
        this.number = number;
        this.curve = curve;
        this.layer = layer;
        offset = new Vector2(offsetX, offsetY);
    }
}
