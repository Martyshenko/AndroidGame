using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMark : Character {

  

    [SerializeField]
    float moveSpeed;

   

    void Start()
    {
        evil = true;
        SetKinematic(true);
        moveSpeed = Random.Range(1, 10);
        rb.useFullKinematicContacts = true;
        rb.velocity = new Vector2(1f * moveSpeed, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Line")
        {

            KillCharacter();
            Destroy(gameObject,5);
        }
    }
}
