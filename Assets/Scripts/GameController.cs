using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed;

    public Vector3 position;

    
	
	void Awake () {

        position = new Vector3(5.1f, 0f, 0f);

        scrollSpeed = -6f;

        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
		
	}



    // Update is called once per frame
    void Update () {

        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
		
	}


    public void GameOver()
    {
       // gameOverText.SetActive(true);
        gameOver = true;
        scrollSpeed = 0;
    }
}
