using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class EnemyPoint
{
    private Transform worldPoint;
    private bool taken = false;

    public EnemyPoint(Transform transform)
    {
        WorldPoint = transform;
        Taken = false;
    }

    public Transform WorldPoint { get => worldPoint; set => worldPoint = value; }
    public bool Taken { get => taken; set => taken = value; }
}
public class EnemyBox : MonoBehaviour
{
    [SerializeField] private Transform boxTopLeftBound;
    [SerializeField] private Transform boxBottomRightBound;
    [SerializeField] private List<Vector2Int> pointGrids;
    [SerializeField] private Transform pointPrefab;
    private List<List<EnemyPoint>> _points = new();
    private void Start()
    {
        Vector2 downRightPos = boxBottomRightBound.position;
        Vector2 upLeftPos = boxTopLeftBound.position;
        var layerOffset = 0;
        var layerTotal = 0;
        foreach (Vector2Int pointGrid in pointGrids)
        {
            layerTotal += pointGrid.y;
        }
        foreach (Vector2Int pointGrid in pointGrids) {
            {
                for (var i = 0; i < pointGrid.x; i++)
                {
                    _points.Add(new List<EnemyPoint>());
                    for (var j = 0; j < pointGrid.y; j++)
                    {
                        var pointPos = new Vector2(upLeftPos.x + i * (downRightPos.x - upLeftPos.x) / (pointGrid.x - 1), upLeftPos.y + (j + layerOffset) * (downRightPos.y - upLeftPos.y) / (layerTotal - 1));
                        _points[i].Add(new EnemyPoint(Instantiate(pointPrefab, pointPos, Quaternion.identity, transform)));
                    }
                }
                layerOffset += pointGrid.y;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (boxTopLeftBound == null || boxBottomRightBound == null)
        {
            return;
        }

        Vector2 downRightPos = boxBottomRightBound.position;
        Vector2 upLeftPos = boxTopLeftBound.position;

        Gizmos.DrawLine(downRightPos, new Vector2(downRightPos.x, upLeftPos.y));
        Gizmos.DrawLine(downRightPos, new Vector2(upLeftPos.x, downRightPos.y));
        Gizmos.DrawLine(upLeftPos, new Vector2(downRightPos.x, upLeftPos.y));
        Gizmos.DrawLine(upLeftPos, new Vector2(upLeftPos.x, downRightPos.y));
        var layerOffset = 0;
        var layerTotal = 0;
        foreach (Vector2Int pointGrid in pointGrids)
        {
            layerTotal += pointGrid.y;
        }
        foreach (Vector2Int pointGrid in pointGrids)
        {
            for (var i = 0; i < pointGrid.x; i++)
            {
                for (var j = 0; j < pointGrid.y; j++)
                {
                    var pointPos = new Vector2(upLeftPos.x + i * (downRightPos.x - upLeftPos.x) / (pointGrid.x - 1), upLeftPos.y + (j + layerOffset) * (downRightPos.y - upLeftPos.y) / (layerTotal - 1));
                    Gizmos.DrawSphere(pointPos, 0.1f);
                }
            }
            layerOffset += pointGrid.y;
        }
    }

    internal EnemyPoint PickPoint(int layer)
    {
        layer = Mathf.Clamp(layer, 0, pointGrids.Count - 1);
        var pointGrid = pointGrids[layer];
        Vector2Int middle = new Vector2Int(pointGrid.x / 2, pointGrid.y / 2);
        Vector2Int pickedPoint = new Vector2Int(-1, -1);
        float minDistance = float.MaxValue;

        var layerTotal = pointGrids.Take(layer).Sum(grid => grid.y);

        // Bounding box limits
        Vector2Int? boundingBoxMin = null;
        Vector2Int? boundingBoxMax = null;

        // Delimitates rectangular area containing all currently taken spots
        for (int i = 0; i < pointGrid.x; i++)
        {
            for (int j = 0; j < pointGrid.y; j++)
            {
                if (_points[i][j + layerTotal].Taken)
                {
                    if (boundingBoxMin == null)
                    {
                        boundingBoxMin = new Vector2Int(i, j);
                        boundingBoxMax = new Vector2Int(i, j);
                    }
                    else
                    {
                        boundingBoxMin = new Vector2Int(Math.Min(boundingBoxMin.Value.x, i), Math.Min(boundingBoxMin.Value.y, j));
                        boundingBoxMax = new Vector2Int(Math.Max(boundingBoxMax.Value.x, i), Math.Max(boundingBoxMax.Value.y, j));
                    }
                }
            }
        }

        //Finds closest spot to center of enemy box
        for (int i = 0; i < pointGrid.x; i++)
        {
            for (int j = 0; j < pointGrid.y; j++)
            {
                if (!_points[i][j + layerTotal].Taken)
                {
                    //If there's a vacant spot to fill in the center rectangle, it should ignore any spots outside it
                    if (boundingBoxMin.HasValue && boundingBoxMax.HasValue)
                    {
                        if (ContainsFalseValuesInBoundingBox(boundingBoxMin.Value, boundingBoxMax.Value, layerTotal) &&
                            (i < boundingBoxMin.Value.x || i > boundingBoxMax.Value.x || j < boundingBoxMin.Value.y || j > boundingBoxMax.Value.y))
                        {
                            continue;
                        }
                    }

                    //Calculates distance from center of box and picks point if closer than the current minimum
                    float distance = Mathf.Abs(i - middle.x) + Mathf.Abs(j - middle.y);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        pickedPoint = new Vector2Int(i, j);
                    }
                }
            }
        }
        //Offsets y position by the total size of the layers above it
        pickedPoint.y += layerTotal;

        //Marks the picked spot as taken
        _points[pickedPoint.x][pickedPoint.y].Taken = true;


        return _points[pickedPoint.x][pickedPoint.y];
    }

    //Checks if rectangular area contains any non-taken spots
    private bool ContainsFalseValuesInBoundingBox(Vector2Int boundingBoxMin, Vector2Int boundingBoxMax, int layerTotal)
    {
        for (int i = boundingBoxMin.x; i <= boundingBoxMax.x; i++)
        {
            for (int j = boundingBoxMin.y; j <= boundingBoxMax.y; j++)
            {
                if (!_points[i][j+layerTotal].Taken)
                {
                    return true;
                }
            }
        }

        return false;
    }


}
