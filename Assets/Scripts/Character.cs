using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected bool evil = false;

    public bool grounded;
    protected Vector2 groundNormal;

    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;


    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected Rigidbody2D rb;

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   protected void SetKinematic(bool newValue)
    {
        //Get an array of components that are of type Rigidbody
        Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody2D rb in bodies)
        {
            rb.isKinematic = newValue;
            
        }
    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        if (GameController.instance.gameOver && evil)
        {
            Invoke("KillCharacter", 2f);
        }

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        
        if (distance > minMoveDistance)
        {

            int count = rb.Cast(move,contactFilter, hitBuffer, distance+shellRadius);
            
            hitBufferList.Clear();
            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                if (!(hitBufferList[i].rigidbody.tag == "maBone"))
                {
                    Vector2 currentNormal = hitBufferList[i].normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }


                    float projection = Vector2.Dot(velocity, currentNormal);

                    if (projection < 0)
                    {
                        velocity = velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

        }

        
        rb.position = rb.position + move.normalized*distance;
        
    }



    protected void KillCharacter()
    {
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

  
}
