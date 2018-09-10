using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public GameObject linePrefab;
    private GameObject lineGO;
    Rect rect;
    Vector2 firstPoint;

    // Use this for initialization
    void Start () {
        rect = takeCubePosition();
    }

    void FixedUpdate()
    {

        // add || inMenu when there will be one
        if (rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
             
            Vector2 lastPoint;

            if (Input.GetMouseButtonDown(0))
            {

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                firstPoint = mousePos;


            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lastPoint = mousePos;
                lineGO = Instantiate(linePrefab, lastPoint, Quaternion.FromToRotation(new Vector3(-1, 0), lastPoint-firstPoint));
                EndDrawing();
            }

        }
        
    }


    private void EndDrawing()
    {



    }

    private Rect takeCubePosition()
    {
        
        Bounds bounds = GetComponent<Renderer>().bounds;

        Rect rect = new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);


        return rect;
    }
}
