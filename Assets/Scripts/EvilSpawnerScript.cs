using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawnerScript : MonoBehaviour {

    public GameObject Evil;
    public float spawnRate = 6f;
    float nextSpawn = 0.0f;
	
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn && !GameController.instance.gameOver)
        {
            nextSpawn = Time.time + spawnRate;

            
            GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject("EvilMark");
            if (projectile!=null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);
            }
            //Instantiate(Evil, transform.position,Quaternion.identity);
        }
	}
}
