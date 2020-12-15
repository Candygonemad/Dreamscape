using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {

    [HideInInspector]
    public static bool bookCollected;
    [HideInInspector]
    public static bool buttonCollected;
    [HideInInspector]
    public static bool handsCollected;
    [HideInInspector]
    public static int keysFound;
    [HideInInspector]
    public static bool keyCardCollected;

    //Make sure that the player starts off with none of the objects
	void Start ()
    {
        bookCollected = false;
        buttonCollected = false;
        handsCollected = false;
        keysFound = 0;
        keyCardCollected = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Book collected: " + bookCollected);
	}
}
