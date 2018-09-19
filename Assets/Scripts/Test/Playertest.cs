using UnityEngine;
using System.Collections;


public class Playertest : CharacterTest {

    void Awake()
    {
        moveSpeed = 6;
    }



    protected override Vector2 Movement()
    {

        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 input = Vector2.right;
        return input;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "EvilMark"|| collision.gameObject.tag == "BigLine")
        {
            
            GameController.instance.GameOver();
        }

    }

}