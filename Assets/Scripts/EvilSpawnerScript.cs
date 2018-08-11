using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawnerScript : MonoBehaviour {

    public GameObject Evil;
    public float spawnRate = 6f;
    float nextSpawn = 0.0f;
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn && !GameController.instance.gameOver)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(Evil, transform.position,Quaternion.identity);
        }
	}
}
