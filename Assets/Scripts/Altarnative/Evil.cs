using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : Player {


    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        controller = GetComponent<Controller2D>();

        moveSpeed = Random.Range(1, 2);

    }


    private void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }


        if (jump > 0)
        {
            velocity.y = jumpVelocity;
            jump = 0;
        }

        velocity.x = moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag.Equals("Line"))
        {
            Debug.Log("Character has been mudered in " + collision.gameObject.transform.position);
           KillCharacter();
            GetComponent<Scroll>().enabled = true;
            Destroy(gameObject, 5);
        }
    }
}
