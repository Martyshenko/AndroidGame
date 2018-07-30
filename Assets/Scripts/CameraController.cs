using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Mark mark;

    private Vector3 lastMarkPosition;

    private float distanceToMove;
	// Use this for initialization
	void Start () {

        mark = FindObjectOfType<Mark>();
        lastMarkPosition = mark.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameController.instance.gameOver)
        {
            distanceToMove = mark.transform.position.x - lastMarkPosition.x;


            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

            lastMarkPosition = mark.transform.position;
        }
	}
}
