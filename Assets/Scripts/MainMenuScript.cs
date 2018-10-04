using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene 
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    //<summary>  
    //Will load a new scene upon being called   
    //</summary>   
    //<param name="levelName">The name of the level we want to go to</param> 

    public Toggle toggle;

    private void Start()
    {
        if (PlayerPrefs.GetInt("2Players") == 1)
        {
            toggle.isOn = true;
        }
        else
            toggle.isOn = false;

    }

    public void IsTwoPlayers()
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("2Players", 1);

        }else
            PlayerPrefs.SetInt("2Players", 0);
    }

   

    

    public void LoadLevel(string levelName)   
    {        SceneManager.LoadScene(levelName);    }
}
