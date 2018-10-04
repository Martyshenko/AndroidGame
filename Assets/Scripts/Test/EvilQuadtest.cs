using UnityEngine;
using System.Collections;


public class EvilQuadtest : CharacterTest
{

    void Start()
    {
         moveSpeed = Random.Range(7, 10);
    }
    

    

    protected override Vector2 Movement()
    {
        
        Vector2 input = Vector2.right;
        return input;
    }

    protected override void JumpingAndDucking()
    {
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
    }

    protected override void IsItFallingOf()
    {
        if (transform.position.y < GameController.instance.lowestDeathBoundery)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

       if(collision.gameObject.tag == "BigLine")
        {
            gameObject.SetActive(false);
        }

    }
}