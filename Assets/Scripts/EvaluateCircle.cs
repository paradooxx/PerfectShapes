using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateCircle : MonoBehaviour
{
    private float centerX;
    private float centerY;
    private float totalRadius = 0;
    private float variance;
    private List<Vector2> drawPoints = new List<Vector2>();

    private void Eval()
    {
        foreach (Vector2 point in drawPoints)
        {
            centerX += point.x;
            centerY += point.y;
        }
        centerX /= drawPoints.Count;
        centerY /= drawPoints.Count;

        foreach (Vector2 point in drawPoints)
        {
            float distance = Vector2.Distance(new Vector2(centerX, centerY), point);
            totalRadius += distance;
        }

        float averageRadius = totalRadius / drawPoints.Count;

        foreach (Vector2 point in drawPoints)
        {
            float distance = Vector2.Distance(new Vector2(centerX, centerY), point);
            variance += Mathf.Pow(distance - averageRadius, 2);
        }

        float circularScore = Mathf.Sqrt(variance / drawPoints.Count);
        Debug.Log("Score: " + circularScore);
    }
}
