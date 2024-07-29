using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyLine : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShape;
    [SerializeField] private EnemyBox enemyBox;

    public SpriteShapeController SpriteShape { get => spriteShape; set => spriteShape = value; }
    public EnemyBox EnemyBox { get => enemyBox; set => enemyBox = value; }
}
