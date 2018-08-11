using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
            
            Debug.Log(collision);

          //collision.rigidbody.isKinematic = true;
          // collision.transform.Translate(0.30F * -Vector2.right);
          //  Debug.Log("collision");
        }
    }
}
