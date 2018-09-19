using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour {

    private Rigidbody2D objectRigidbody;
    public float speed;
    

    // Use this for initialization
    void OnEnable()
    {
        
        objectRigidbody = transform.GetComponent<Rigidbody2D>();
        objectRigidbody.velocity = -(transform.up* speed);
        
    }

    void OnBecameInvisible()
    {

        RemoveObject();
             
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Ground")
        {
            objectRigidbody.simulated = false;
            objectRigidbody.velocity = Vector2.zero;
            objectRigidbody.isKinematic = true;
            transform.parent = other.gameObject.transform;
            
        }else if(other.gameObject.tag == "EvilMark")
        {
            RemoveObject();
        }
    }

    void RemoveObject()
    {
        if (gameObject.activeInHierarchy == true)
        {
            transform.parent = null;
        }
        objectRigidbody.isKinematic = false;
        objectRigidbody.simulated = true;
        gameObject.SetActive(false);
    }


}
