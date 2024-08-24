using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector2> drawPoints = new List<Vector2>();
    private float totalRadius = 0;
    private float variance;
    private float maxVariance = 100;
    private float maxCenterOffset = 10f;

    [SerializeField] private TMP_Text statusText;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            drawPoints.Clear();
            lineRenderer.positionCount = 0;
        }

        if(Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawPoints.Add(mousePosition);
            lineRenderer.positionCount = drawPoints.Count;
            lineRenderer.SetPosition(drawPoints.Count - 1, mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            EvaluateCircle();
        }
    }

    private void EvaluateCircle()
    {
        if (drawPoints.Count == 0) return;

        float flDistance = Vector2.Distance(drawPoints[0], drawPoints[drawPoints.Count - 1]);
        if (flDistance > 0.15f)
        {
            statusText.text = "Draw Complete Circle!";
            return;
        }

        totalRadius = 0;

        Vector2 center = Vector2.zero;

        float fullRadius = Vector2.Distance(drawPoints[0], center) * drawPoints.Count;

        foreach (Vector2 point in drawPoints)
        {
            float distance = Mathf.Abs((fullRadius / drawPoints.Count) - Vector2.Distance(point, center));
            totalRadius += distance;
        }

        float percentage = (fullRadius - totalRadius) / fullRadius * 100;
        statusText.text  = percentage.ToString() + " %";
    }
}
