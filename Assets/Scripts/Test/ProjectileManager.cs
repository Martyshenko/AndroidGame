using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileManager : MonoBehaviour {


    public GameObject linePrefab;
    private GameObject lineGO;
    Rect rect;
    Vector2 firstPoint;


    FireBarScript IfReadyToShoot;

    

    public TargetPosition positions;
    Vector2 ChangeInPosition;

    // Use this for initialization
    void Start () {

        

        rect = takeCubePosition();
        
          positions.targetPosition = rect.position;

        IfReadyToShoot = FindObjectOfType<FireBarScript>();

    }

    void Update()
    {
        UpdateChangeInPos();
        rect = takeCubePosition();
        
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
                
                
                Shoot( lastPoint);
            }

        }

        if (firstPoint != null)
        {
            
            firstPoint.Set(firstPoint.x + ChangeInPosition.x, firstPoint.y + ChangeInPosition.y);
            
        }

    }


    private void Shoot( Vector2 lastPoint)
    {
        if (IfReadyToShoot.ready)
        {

            GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject("BigLine");

            if (projectile != null)
            {
                projectile.transform.position = lastPoint;

                projectile.transform.up = lastPoint - firstPoint;
                projectile.transform.rotation = Quaternion.FromToRotation(new Vector3(0, -1), lastPoint - firstPoint);

                projectile.SetActive(true);

                IfReadyToShoot.shooted = true;

            }


        }
        
    }

    private void UpdateChangeInPos()
    {

        positions.targetPositionOld = positions.targetPosition;
        positions.targetPosition = rect.position;
        
         ChangeInPosition = -(positions.targetPositionOld - positions.targetPosition);

     

    }
    private Rect takeCubePosition()
    {
        
        Bounds bounds = GetComponent<Renderer>().bounds;

        Rect rect = new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);


        return rect;
    }

    public struct TargetPosition
    {
        public Vector2 targetPosition, targetPositionOld;
    }
}
