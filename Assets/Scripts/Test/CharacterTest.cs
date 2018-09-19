using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2Dtest))]
public abstract class CharacterTest : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    protected float moveSpeed = 6;

 


    protected int jump = 0;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2Dtest controller;

    void Start()
    {
        controller = GetComponent<Controller2Dtest>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + "  Jump " + maxJumpVelocity);
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = Movement();

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }


        if (jump > 0 && controller.collisions.below)
        {
            switch (jump)
            {
                case 1:
                    velocity.y = minJumpVelocity;
                    jump = 0;

                    break;
                case 2:
                    velocity.y = maxJumpVelocity;
                    jump = 0;
                    break;

            }

        }

      float targetVelocityX = input.x * moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {


        switch (collision.tag)
        {
            case "SmallObstacle":
                jump = 1;
                break;
            case "BigObstacle":
                jump = 2;
                break;

        }

    }
  
    protected abstract Vector2 Movement();
   

}