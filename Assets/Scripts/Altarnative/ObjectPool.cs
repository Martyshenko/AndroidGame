using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public int boxPoolSize = 5;
    public GameObject boxPrefab;

    private GameObject[] boxes;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);

    private float timeSinceLastSpawned;
    public float spawnRate = 4f;
    public float boxMin = 30f;
    public float boxMax = 51f;

    private float spawnYPosition = -1.8f;

    private int currentBox = 0;

	// Use this for initialization
	void Start () {
        boxes = new GameObject[boxPoolSize];
        for (int i = 0; i < boxPoolSize; i++)
        {
            boxes[i] = (GameObject)Instantiate(boxPrefab, objectPoolPosition, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {

        timeSinceLastSpawned += Time.deltaTime;

        if (GameController.instance.gameOver == false&& timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnXPosition = Random.Range(boxMin,boxMax);
            boxes[currentBox].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentBox++;
            if (currentBox >= boxPoolSize)
            {
                currentBox = 0;
            }
        }

		
	}
}
