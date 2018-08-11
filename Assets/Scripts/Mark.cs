using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : Character {

    
    void Start()
    {

        SetKinematic(true);
       rb.useFullKinematicContacts = true;


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
}
