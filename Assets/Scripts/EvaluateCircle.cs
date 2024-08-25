using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvaluateCircle : MonoBehaviour
{
    private float centerX = 0f;
    private float centerY = 0f;
    private float totalRadius = 0f;
    private Vector2 center = Vector2.zero;

    public void EvalCircle(List<Vector2> drawPoints, TMP_Text statusText)
    {
        if (drawPoints.Count == 0) return;

        totalRadius = 0;

        Vector2 drawnCenter = NewCenter(drawPoints);
        float idealRadius = Vector2.Distance(drawPoints[0], center);
        float fullRadius = idealRadius * drawPoints.Count;

        foreach (Vector2 point in drawPoints)
        {
            float distance = Mathf.Abs(idealRadius - Vector2.Distance(point, drawnCenter));
            totalRadius += distance;
        }

        if (IsPointCircle(drawPoints) || !IsFullCircle(drawPoints))
        {
            statusText.text = "Incomplete Circle";
            return;
        }

        float checkRadius = Vector2.Distance(drawnCenter, center);
        float drawnRadius = DrawnRadius(drawPoints, drawnCenter);

        if (checkRadius > drawnRadius)
        {
            statusText.text = "Encircle the center";
            return;
        }
        
        float percentage = (fullRadius - totalRadius) / fullRadius * 100;
        statusText.text = percentage.ToString("F2") + " %";
    }

    private Vector2 NewCenter(List<Vector2> points)
    {
        foreach (Vector2 point in points)
        {
            centerX += point.x;
            centerY += point.y;
        }
        centerX /= points.Count;
        centerY /= points.Count;

        return new Vector2(centerX, centerY);
    }

    private float DrawnRadius(List<Vector2> drawPoints, Vector2 center)
    {
        float radiusSum = 0f;
        foreach (Vector2 point in drawPoints)
        {
            float distance = Vector2.Distance(center, point);
            radiusSum += distance;
        }
        float averageRadius = radiusSum / drawPoints.Count;
        return averageRadius;
    }

    private bool IsFullCircle(List<Vector2> drawPoints)
    {
        float flDistance = Vector2.Distance(drawPoints[0], drawPoints[drawPoints.Count - 1]);
        return flDistance <= 0.15f;
    }

    private bool IsPointCircle(List<Vector2> drawPoints)
    {
        return drawPoints.Count < 20;
    }
    
}
