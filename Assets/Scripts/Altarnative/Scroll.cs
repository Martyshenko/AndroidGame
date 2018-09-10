using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {


    // Update is called once per frame  
    void Update()
    {

        //transform.Translate(-1*GameController.instance.position);
        transform.position = new Vector2(transform.position.x + GameController.instance.scrollSpeed * Time.deltaTime, transform.position.y);


    }
}

