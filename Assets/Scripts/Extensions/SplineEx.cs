using UnityEngine;
using UnityEngine.U2D;

public static class SplineEx
{
    private const int numSegments = 100;

    public static Vector2 GetPoint(this Spline spline, float progress)
    {
        var length = spline.GetPointCount();
        var i = Mathf.Clamp(Mathf.CeilToInt((length - 1) * progress), 0, length - 1);

        var t = progress * (length - 1) % 1f;
        if (i == length - 1 && progress >= 1f)
            t = 1;

        var prevIndex = Mathf.Max(i - 1, 0);

        var _p0 = new Vector2(spline.GetPosition(prevIndex).x, spline.GetPosition(prevIndex).y);
        var _p1 = new Vector2(spline.GetPosition(i).x, spline.GetPosition(i).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(prevIndex).x, spline.GetRightTangent(prevIndex).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(i).x, spline.GetLeftTangent(i).y);

        return BezierUtility.BezierPoint(
            new Vector2(_rt.x, _rt.y),
            new Vector2(_p0.x, _p0.y),
            new Vector2(_p1.x, _p1.y),
            new Vector2(_lt.x, _lt.y),
            t
        );
    }

    public static Vector2 GetPointX(this Spline spline, float progress)
    {
        var totalLength = CalculateTotalLength(spline);

        if (totalLength <= 0f)
        {
            // Handle division by zero or invalid spline
            return Vector2.zero;
        }

        var length = spline.GetPointCount();
        var cumulativeDistance = progress * totalLength;

        var i = 0;
        var j = 0;
        float cumulativeLength = 0f;
        float cumulativeSegmentLength = 0f;

        while (i < length - 1 && cumulativeLength + SegmentLength(spline, i) < cumulativeDistance)
        {
            cumulativeLength += SegmentLength(spline, i);
            i++;
        }

        while (j < numSegments && cumulativeLength + cumulativeSegmentLength + SubSegmentLength(spline, i, j) < cumulativeDistance)
        {
            cumulativeSegmentLength += SubSegmentLength(spline, i, j);
            j++;
        }

        var remainingDistance = cumulativeDistance - (cumulativeLength+cumulativeSegmentLength);

        var tSegment = Mathf.InverseLerp(0, SubSegmentLength(spline, i, j), remainingDistance);
        tSegment /= numSegments;
        tSegment += 1f / numSegments * j;

        var _p0 = new Vector2(spline.GetPosition(i).x, spline.GetPosition(i).y);
        var _p1 = new Vector2(spline.GetPosition(i + 1).x, spline.GetPosition(i + 1).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(i).x, spline.GetRightTangent(i).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(i + 1).x, spline.GetLeftTangent(i + 1).y);

        return BezierUtility.BezierPoint(
            new Vector2(_rt.x, _rt.y),
            new Vector2(_p0.x, _p0.y),
            new Vector2(_p1.x, _p1.y),
            new Vector2(_lt.x, _lt.y),
            tSegment
        );
    }

    public static float CalculateTotalLength(this Spline spline)
    {
        var totalLength = 0f;
        var length = spline.GetPointCount();

        for (int i = 0; i < length - 1; i++)
        {
            totalLength += SegmentLength(spline, i);
        }

        return totalLength;
    }

    private static float SegmentLength(this Spline spline, int index)
    {
        float length = 0; 
        var _p0 = new Vector2(spline.GetPosition(index).x, spline.GetPosition(index).y);
        var _p1 = new Vector2(spline.GetPosition(index+1).x, spline.GetPosition(index + 1).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(index).x, spline.GetRightTangent(index).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(index + 1).x, spline.GetLeftTangent(index + 1).y);

        for (int i = 0; i < numSegments; i++)
        {
            float t1 = (float)i / numSegments;
            float t2 = (float)(i + 1) / numSegments;

            Vector2 p1 = BezierUtility.BezierPoint(
            new Vector2(_rt.x, _rt.y),
            new Vector2(_p0.x, _p0.y),
            new Vector2(_p1.x, _p1.y),
            new Vector2(_lt.x, _lt.y),
            t1
            );

            Vector2 p2 = BezierUtility.BezierPoint(
            new Vector2(_rt.x, _rt.y),
            new Vector2(_p0.x, _p0.y),
            new Vector2(_p1.x, _p1.y),
            new Vector2(_lt.x, _lt.y),
            t2
            );

            length += Vector2.Distance(p1, p2);
        }
        return length;
    }

    private static float SubSegmentLength(this Spline spline, int index, int segmentNum)
    {
        var _p0 = new Vector2(spline.GetPosition(index).x, spline.GetPosition(index).y);
        var _p1 = new Vector2(spline.GetPosition(index + 1).x, spline.GetPosition(index + 1).y);
        var _rt = _p0 + new Vector2(spline.GetRightTangent(index).x, spline.GetRightTangent(index).y);
        var _lt = _p1 + new Vector2(spline.GetLeftTangent(index + 1).x, spline.GetLeftTangent(index + 1).y);

        float t1 = (float)segmentNum / numSegments;
        float t2 = (float)(segmentNum + 1) / numSegments;

        Vector2 p1 = BezierUtility.BezierPoint(
        new Vector2(_rt.x, _rt.y),
        new Vector2(_p0.x, _p0.y),
        new Vector2(_p1.x, _p1.y),
        new Vector2(_lt.x, _lt.y),
        t1
        );

        Vector2 p2 = BezierUtility.BezierPoint(
        new Vector2(_rt.x, _rt.y),
        new Vector2(_p0.x, _p0.y),
        new Vector2(_p1.x, _p1.y),
        new Vector2(_lt.x, _lt.y),
        t2
        );

        return Vector2.Distance(p1, p2);
    }
}