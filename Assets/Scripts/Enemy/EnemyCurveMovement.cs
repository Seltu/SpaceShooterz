using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyCurveMovement: EnemyMovement<CurveEnemyAi>
{
    private EnemyLine curve;
    private float _bezierTimer;
    private Vector2 _previousPoint;
    private Vector2 _offset;
    private EnemyPoint boxPoint;
    private float splineLength;
    private bool hasBox;
    protected override void OnEnable()
    {
        ai.OnSetMovement.AddListener(SetMovement);
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        ai.OnSetMovement.RemoveListener(SetMovement);
        base.OnDisable();
    }

    private void SetMovement(EnemyLine waveCurve, Vector2 waveOffset, int layer)
    {
        curve = waveCurve;
        _offset = waveOffset;
        gameObject.transform.position = curve.SpriteShape.spline.GetPoint(_bezierTimer);
        splineLength = curve.SpriteShape.spline.CalculateTotalLength();
        hasBox = waveCurve.EnemyBox != null;
        if (hasBox)
        {
            boxPoint = waveCurve.EnemyBox.PickPoint(layer);
        }
    }

    protected override void Move()
    {
        if (_bezierTimer <= 1f)
        {
            var spline = curve.SpriteShape.spline;
            gameObject.transform.position = spline.GetPointX(_bezierTimer) + _offset;
            _bezierTimer += Time.deltaTime * ai.GetStats().GetMovementSpeed() * 0.02f;
        }
        else
        {
            if (hasBox)
            {
                _bezierTimer += Time.deltaTime;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, boxPoint.WorldPoint.position, ai.GetStats().GetMovementSpeed() * Time.deltaTime);
            }
            else
                ai.Eliminate();
        }
    }
}
