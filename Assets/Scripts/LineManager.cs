using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LineManager : MonoBehaviour
{

    public GameObject linePrefab;
    public List<GameObject> drawnLines;
    private GameObject lineGO;

    public GameObject shootingZone;

    Rect rect;

    LineScript activeLine;

    private void Start()
    {
         rect = takeCubePosition();
        
    }


    void FixedUpdate()
    {
        rect = takeCubePosition();
        // add || inMenu when there will be one
        if (rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            if (Input.GetMouseButtonDown(0))
            {


                lineGO = Instantiate(linePrefab);
                activeLine = lineGO.GetComponent<LineScript>();
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndDrawing();
            }

            if (activeLine != null)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePos);

            }
        }
        else if (activeLine != null)
        {
            EndDrawing();
        }
    }

        
        private void EndDrawing()
    {
        
        if(lineGO == null) { 
        activeLine = null;
        }
        else
        {
            Vector2 direction = activeLine.GetLineDirection();
            activeLine = null;

            drawnLines.Add(lineGO);
            LaunchLine(lineGO, direction);
            lineGO = null;
            // Test thing
           DestroyLine();
        }

        
    }


    private void DestroyLine(){
            if (drawnLines.Count > 0) { 
                Destroy(drawnLines.First(), 3);
                drawnLines.Remove(drawnLines.First());

        }

    }

    private void LaunchLine(GameObject line, Vector2 direction)
    {
        Rigidbody2D rb = line.GetComponent<Rigidbody2D>();
            rb.AddForce(direction*7,ForceMode2D.Impulse);
        rb.gravityScale = 2;
        rb.useAutoMass = true;

    }


    private Rect takeCubePosition()
    {
        string bound = shootingZone.GetComponent<Renderer>().bounds.ToString();

        string[] substrings = (bound.Replace("Center: (", "").Replace("Extents: (", "").Replace(")", "").Replace("(", "")).Split(',');

        float[] numbers = new float[4];
        numbers[0] = float.Parse(substrings[0]);
        numbers[1] = float.Parse(substrings[1]);
        numbers[2] = float.Parse(substrings[3]);
        numbers[3] = float.Parse(substrings[4]);

        
      Rect  rect = new Rect(numbers[0]-numbers[2], numbers[1]-numbers[3], numbers[2]*2, numbers[3]*2);
        
        
        return rect;
    }
    
    

}