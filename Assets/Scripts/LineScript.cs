using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour {

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;

    List<Vector2> points;

    public void UpdateLine(Vector2 touchPos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(touchPos);
            return;
        }

        if (Vector2.Distance(points.Last(), touchPos) > .1f)
            SetPoint(touchPos);
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
            edgeCol.points = points.ToArray();
    }

    public Vector2 GetLineDirection()
    {
       return points.Last() - points.First();
    }

}
