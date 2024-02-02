using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyCurveMovement: EnemyMovement<CurveEnemyAi>
{
    private SpriteShapeController curve;
    private float _bezierTimer;
    private Vector2 _previousPoint;
    private Vector2 _offset;
    private float splineLength;
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

    private void SetMovement(SpriteShapeController waveCurve, Vector2 waveOffset)
    {
        curve = waveCurve;
        _offset = waveOffset;
        gameObject.transform.position = curve.spline.GetPoint(_bezierTimer);
        splineLength = waveCurve.spline.CalculateTotalLength();
    }

    protected override void Move()
    {
        var spline = curve.spline;
        gameObject.transform.position = spline.GetPointX(_bezierTimer) + _offset;
        _bezierTimer += Time.deltaTime * ai.GetStats().GetMovementSpeed() / splineLength;
        if (_bezierTimer <= 1f) return;
        ai.Eliminate();
    }
}
