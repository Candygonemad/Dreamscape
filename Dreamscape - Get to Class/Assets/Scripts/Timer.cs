using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Timer : MonoBehaviour {

    public Text timeText;
    private float timeLeft;
    private int minutes;
    private int seconds;
    public GameObject player;
    // Use this for initialization
    void Start () {
        timeLeft = 180;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        minutes = (int)(timeLeft) / 60;
        seconds = (int)(timeLeft) % 60;
        
        if(timeLeft > 0)
            timeText.text = string.Format("{0}:{1:00} Until Class", minutes, seconds);
        else if(timeLeft <= 0)
        {
            timeText.text = "You Slept Through Your Class";
            GameOver();
        }
	}

    public void GameOver()
    {
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().LockMouse();
        Debug.Log("Called Game Over");
    }
}
