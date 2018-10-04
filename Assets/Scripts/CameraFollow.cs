﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Controller2Dtest target;
    public Vector2 focusAreaSize;

    public float verticalOffset;

    FocusArea focusArea;

    void Start()
    {
        focusArea = new FocusArea(target.GetComponent<Collider2D>().bounds, focusAreaSize);
    }

    private void LateUpdate()
    {
        focusArea.Update(target.GetComponent<Collider2D>().bounds);
        Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 velocity;
        public Vector2 center;
        float left, right;
        float top, bottom;

        

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left+right)/2,(top+bottom)/2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }

            top += shiftY;
            bottom += shiftY;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }

    }

    
}
