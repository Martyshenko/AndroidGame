using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
   
    protected float moveSpeed = 6f;
    protected int jump = 0;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float jumpVelocity;
    public float gravity;

   protected Vector2 velocity;


    protected Controller2D controller;

    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        controller = GetComponent<Controller2D>();

    }

    private void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        

        if(jump>0)
        {
            velocity.y = jumpVelocity;
            jump = 0;
        }

        velocity.x = moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "SmallObstacle":
                jump = 1;
                break;
            case "MediumBox":
                jump = 2;
                break;
            case "BigBox":
                jump = 3;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag.Equals("Line") || collision.gameObject.tag.Equals("EvilMark"))
        {
            Debug.Log(collision);
            KillCharacter();
            GameController.instance.GameOver();
        }
    }

    protected void KillCharacter()
    {
        moveSpeed = 0;
       controller.SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}