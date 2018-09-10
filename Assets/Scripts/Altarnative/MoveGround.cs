using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {

    float leftWayPointX = -45f, rightWayPointX = 45f;
	

	// Update is called once per frame  
	void Update () {
     
        transform.position = new Vector2(transform.position.x + GameController.instance.scrollSpeed * Time.deltaTime, transform.position.y);

        if (transform.position.x < leftWayPointX)
            transform.position = new Vector2(rightWayPointX, transform.position.y);
		
	}
}
