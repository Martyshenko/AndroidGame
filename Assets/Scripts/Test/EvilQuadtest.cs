using UnityEngine;
using System.Collections;


public class EvilQuadtest : CharacterTest
{

    void Awake()
    {
         moveSpeed = Random.Range(7, 10);
    }
    

    

    protected override Vector2 Movement()
    {
        
        Vector2 input = Vector2.right;
        return input;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

       if(collision.gameObject.tag == "BigLine")
        {
            gameObject.SetActive(false);
        }

    }
}