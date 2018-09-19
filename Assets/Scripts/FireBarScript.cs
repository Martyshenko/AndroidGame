using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBarScript : MonoBehaviour {

    Image timerBar;
    
    float timeLeft;

    public bool ready;
    public bool shooted;

    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = 0.1f;
        timerBar.fillAmount = 0;
        shooted = false;
    }

    void Update()
    {
        if (timeLeft < 3)
        {
            timeLeft += Time.deltaTime;
            timerBar.fillAmount = timeLeft / 3;

        }
        else
        {
            ready = true;
            if (shooted) {
                timeLeft = 0.1f;
                timerBar.fillAmount = 0;
                ready = false;
                shooted = false;
            }
        }
    }
}
