using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public int currentLevel = 0; //Used when changing from menu to game scene
    public int menuFocus = 0; //Used when entering the menu scene, to know which menu we are going to focus 
}
