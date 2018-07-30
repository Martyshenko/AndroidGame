using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour {

    float dirX;

    [SerializeField]
    float moveSpeed = 3f;

    Rigidbody2D rb;

    bool moving = true;
    bool facingRight = false;
    

    Vector3 localScale;

    void SetKinematic(bool newValue)
    {
        //Get an array of components that are of type Rigidbody
        Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody2D rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    // Use this for initialization
    void Start () {

        SetKinematic(true);

        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
		
	}

    // Update is called once per frame
    void Update()
    {
         

        if (moving) { 
        if (transform.position.x < -9f)
            dirX = 1f;
        else if (transform.position.x > 9f)
            dirX = -1f;
        }
	}

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = new Vector2(1f * moveSpeed, rb.velocity.y);
        }
    }

    private void LateUpdate()
    {
        //CheckWhereToFace();

    }


    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }

    void KillCharacter()
    {
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        moving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Line")
        {
            
            KillCharacter();
            GameController.instance.GameOver();
        }
    }
}
