using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyCurveMovement: EnemyMovement<CurveEnemyAi>
{
    private EnemyLine _curve;
    private float _bezierTimer;
    private Vector2 _previousPoint;
    private Vector2 _offset;
    private float _splineLength;
    private bool _inBox;
    private float _speedMultiplier = 1f;
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

    private void SetMovement(EnemyLine waveCurve, Vector2 waveOffset, int layer, float speedMultiplier)
    {
        _curve = waveCurve;
        _offset = waveOffset;
        gameObject.transform.position = _curve.SpriteShape.spline.GetPoint(_bezierTimer);
        _splineLength = _curve.SpriteShape.spline.CalculateTotalLength();
        _speedMultiplier = speedMultiplier;
        if (waveCurve.EnemyBox != null)
        {
            ai.SetBoxPoint(waveCurve.EnemyBox.PickPoint(layer));
        }
    }

    protected override void Move()
    {
        if (_bezierTimer <= 1f)
        {
            var spline = _curve.SpriteShape.spline;
            gameObject.transform.position = spline.GetPointX(_bezierTimer) + _offset;
            _bezierTimer += Time.deltaTime * ai.GetStats().GetMovementSpeed() * 0.02f * _speedMultiplier;
        }
        else if (ai.HasBox())
        {
            if (!_inBox)
            {
                _inBox = true;
                ai.SetStopShooting(true);
            }
            _bezierTimer += Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, ai.GetBoxPoint().WorldPoint.position, ai.GetStats().GetMovementSpeed() * Time.deltaTime);
        }
        else if (_inBox)
        {
            gameObject.transform.Translate(Vector2.down*Time.deltaTime*ai.GetStats().GetMovementSpeed());
        }
        else
        {
            ai.Eliminate();
        }
    }
}