using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2Dtest))]
public abstract class CharacterTest : MonoBehaviour
{
    protected float maxJumpHeight = 6;
    protected float minJumpHeight = 4;
    public float timeToJumpApex = .6f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    protected float moveSpeed = 6;

 


    protected int jump = 0;

    float gravity;
    protected float maxJumpVelocity;
    protected float minJumpVelocity;
    protected Vector3 velocity;
    float velocityXSmoothing;

    protected Controller2Dtest controller;

    void Awake()
    {
        controller = GetComponent<Controller2Dtest>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        
    }

    void Update()
    {
       
        IsItFallingOf();

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = Movement();

        JumpingAndDucking();

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
            case "LongObstacle":
                jump = 2;
                break;
        }

    }
  
    protected abstract Vector2 Movement();

    protected abstract void IsItFallingOf();

    protected abstract void JumpingAndDucking();
  

}