using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDodgerTest : CharacterTest {

    public Animator anim;
    BoxCollider2D col;

    bool ducking = false;

    void Start()
    {


        anim = GetComponent<Animator>();

        col = controller.colliderr;

        moveSpeed = Random.Range(7, 10);
    }

    protected override void IsItFallingOf()
    {
        if (transform.position.y < GameController.instance.lowestDeathBoundery)
        {
            gameObject.SetActive(false);
        }
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

    protected override Vector2 Movement()
    {
        Vector2 input = Vector2.right;
        return input;
    }

    public void doJump()
    {
        jump = 2;
    }

    public void doDuck()
    {
        if (!ducking)
        {
            ducking = true;
            anim.SetBool("isDown", true);
            col.size = new Vector2(col.size.x, 0.54f);
            col.offset = new Vector2(col.offset.x, col.offset.y - 0.25f);
            
            Invoke("StandUp", 1);
        }

    }

    public void StandUp()
    {
        anim.SetBool("isDown", false);
        col.offset = new Vector2(col.offset.x, col.offset.y + 0.25f);
        col.size = new Vector2(col.size.x, 1);
        
        ducking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "BigLine")
        {
            gameObject.SetActive(false);
        }

    }

}
