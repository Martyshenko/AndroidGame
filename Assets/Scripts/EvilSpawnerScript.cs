using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawnerScript : MonoBehaviour {

    public LayerMask collisionMask;
    public float spawnRate = 6f;
    float nextSpawn = 0.0f;
    bool groundBelow = true;
    float spawnHeight;

    private void Start()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0.1f, -1), Mathf.Infinity, collisionMask);
        spawnHeight = hit.distance;
        
    }
    // Update is called once per frame
    void Update() {

        if (Time.time > nextSpawn && !GameController.instance.gameOver && groundBelow)
        {


            nextSpawn = Time.time + spawnRate;


            GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject("EvilMark");
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);
            }


            //Instantiate(Evil, transform.position,Quaternion.identity);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), Mathf.Infinity, collisionMask);
        if (hit)
        {
            groundBelow = true;
    
            if (hit.distance > spawnHeight)
                transform.position = new Vector2(transform.position.x, transform.position.y - (hit.distance - spawnHeight));
            else if (hit.distance < spawnHeight)
                transform.position = new Vector2(transform.position.x, transform.position.y + (spawnHeight - hit.distance));
        }
        else
            groundBelow = false;
        
        


    }
}
