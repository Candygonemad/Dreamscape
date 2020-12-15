using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public GameObject player;
    public Text titleText;
    public Text subtitleText;
    public Text eToStart;
    //public Image fadeIn;

	// Use this for initialization
	void Start () {
        //fadeIn.alp
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().LockMouse();
    }
	
	// Update is called once per frame
	void Update () {
      //  fadeIn
		if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1;
            titleText.enabled = false;
            subtitleText.enabled = false;
            player.GetComponent<FirstPersonController>().UnlockMouse();
        }
	}
}
