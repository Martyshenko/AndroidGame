using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Playertest : CharacterTest {

    public Animator anim;
    BoxCollider2D col;
    bool enableJumping = false;

    private void Start()
    {
        moveSpeed = 6;
        if (GameController.instance.twoPlayers)
        {
            //Animator can be moved into singl after testing
            anim = GetComponent<Animator>();

             col = controller.colliderr;

            GetComponent<CircleCollider2D>().enabled = false;
            enableJumping = true;
             
            }
    }
        




    protected override Vector2 Movement()
    {

        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 input = Vector2.right;
        return input;
    }
    protected override void IsItFallingOf()
    {
        if (transform.position.y < GameController.instance.lowestDeathBoundery)
        {
            GameController.instance.GameOver();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "EvilMark"|| collision.gameObject.tag == "BigLine")
        {
            
            //GameController.instance.GameOver();
        }

    }

    protected override void JumpingAndDucking()
    {
        
        if (enableJumping == true)
        {


            if (CrossPlatformInputManager.GetButtonDown("Duck"))
            {
                anim.SetBool("isDown", true);
                col.size = new Vector2(col.size.x, 0.54f);
                col.offset = new Vector2(col.offset.x, col.offset.y-0.25f);
                moveSpeed = moveSpeed / 2;
            }

            if (CrossPlatformInputManager.GetButtonUp("Duck"))
            {
                anim.SetBool("isDown", false);
                col.offset = new Vector2(col.offset.x, col.offset.y + 0.25f);
                col.size = new Vector2(col.size.x, 1);
                moveSpeed = moveSpeed * 2;
            }

                //TESTING !!!!!!!!!!!!!
                //if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
                if (CrossPlatformInputManager.GetButtonDown("Jump") && controller.collisions.below)
            {
                //velocity.y = maxJumpVelocity;
                velocity.y = 20;
                
            }

            //if (Input.GetKeyUp(KeyCode.Space))
            if(CrossPlatformInputManager.GetButtonUp("Jump"))
            {
                
                if (velocity.y > minJumpVelocity)
                {
                    
                    //velocity.y = minJumpVelocity;
                    velocity.y = 9;
                }
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
    }

    public void doJump()
    {
        velocity.y = 20;
    }

    public void doDuck()
    {
        anim.SetBool("isDown", true);
        col.size = new Vector2(col.size.x, 0.54f);
        col.offset = new Vector2(col.offset.x, col.offset.y - 0.25f);
        moveSpeed = moveSpeed / 2;
        Invoke("StandUp", 2);

    }

    public void StandUp()
    {
        anim.SetBool("isDown", false);
        col.offset = new Vector2(col.offset.x, col.offset.y + 0.25f);
        col.size = new Vector2(col.size.x, 1);
        moveSpeed = moveSpeed * 2;
    }
}