using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public bool twoPlayers= false;

    public GameObject jumpButton;
    public GameObject duckButton;

    public float scrollSpeed;

    public float lowestDeathBoundery = -12f;

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

        if (PlayerPrefs.GetInt("2Players") == 1)
        {
            twoPlayers = true;

        }
        else
        {
            jumpButton.SetActive(false);
            duckButton.SetActive(false);

        }

    }



    // Update is called once per frame
    void Update () {

        if(gameOver == true)
            PauseGame();
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {  
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            UnPauseGame();
        }
		
	}


    public void GameOver()
    {
       // gameOverText.SetActive(true);
        gameOver = true;
        
        
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
    }

}
