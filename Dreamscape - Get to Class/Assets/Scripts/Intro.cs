using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Intro : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().LockMouse();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1;
            player.GetComponent<FirstPersonController>().UnlockMouse();
        }
	}
}
