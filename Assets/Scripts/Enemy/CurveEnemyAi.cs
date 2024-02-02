using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class CurveEnemyAi : EnemyAi
{
    [HideInInspector] public UnityEvent<SpriteShapeController, Vector2> OnSetMovement;
}
