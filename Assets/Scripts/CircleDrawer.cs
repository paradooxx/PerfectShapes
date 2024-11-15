using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector2> drawPoints = new List<Vector2>();

    [SerializeField] private EvaluateCircle evaluateCircle;
    [SerializeField] private TMP_Text statusText;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawPoints.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawPoints.Add(mousePosition);
            lineRenderer.positionCount = drawPoints.Count;
            lineRenderer.SetPosition(drawPoints.Count - 1, mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            evaluateCircle.EvalCircle(drawPoints, statusText, lineRenderer);
        }
    }
}
