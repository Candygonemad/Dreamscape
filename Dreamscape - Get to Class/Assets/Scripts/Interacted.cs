using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Interacted : MonoBehaviour {

    //Variables that will be exchanged with other methods
    [HideInInspector]
    public static bool bookInPlace;
    [HideInInspector]
    public static bool handsInPlace;
    public GameObject missingBook;
    public GameObject missingHands;
    public GameObject classroomKey;
    public Animator moveDown;
    public Animator moveUp;
    public Animator doorSwing;
    public GameObject panel;
    public GameObject door;
    public Text winText;
    public GameObject player;
    [HideInInspector]
    public static bool doorActivated;

    AudioSource thisObject;

    public enum UseState
    {
        Gettable, Animatable, Pushable
    };

    public UseState state;
	// Use this for initialization
	void Start () {
        bookInPlace = false;
        handsInPlace = false;
        gameObject.AddComponent<AudioSource>();
        thisObject = gameObject.GetComponent<AudioSource>();
        winText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void GetInteracted()
    {
        
        //Tests if the book is the object being interacted with
        if(CompareTag("book"))
        {
            ItemCollector.bookCollected = true;
        }

        //Test if the hands are the object being interacted with
        if(CompareTag("clock hands"))
        {
            ItemCollector.handsCollected = true;
            Debug.Log(ItemCollector.handsCollected);
        }

        if (CompareTag("key"))
        {
            ItemCollector.keysFound++;
        }

        if (CompareTag("Keycard"))
        {
            ItemCollector.keyCardCollected = true;
        }

        Debug.Log("Keys found: " + ItemCollector.keysFound);

        gameObject.layer = 10;
        foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 10;
        }
    }

    public void Animate()
    {

        //Tests if the bookshelf is being interacted with and the book is collected
        if(CompareTag("bookshelf") && ItemCollector.bookCollected == true)
        {
            bookInPlace = true;
            missingBook.SetActive(true);
            moveDown.SetBool("bookInPlace", true);
            StartCoroutine(WaitFiveSeconds());
        }

        if(CompareTag("finalDoor") && ItemCollector.keysFound == 3)
        {
            doorSwing.SetBool("allKeys", true);
        }


        Debug.Log("Animated");
    }

    public void Push()
    {
        //Debug.Log("Pushed");


        if (CompareTag("desk"))
        {
            // if the spotlight is off
            bool interactable = !transform.GetChild(DeskPuzzle.spotLightIndex).gameObject.active;
            if (interactable)
            {
                thisObject.PlayOneShot(Interact.pushAudio);
                DeskPuzzle.ToggleLights(DeskPuzzle.desks.IndexOf(gameObject));
            }
        }

        //Tests if the clock is being interacted with and the hands are collected
        if(CompareTag("grandfather clock") && ItemCollector.handsCollected == true)
        {
            thisObject.PlayOneShot(Interact.pushAudio);
            handsInPlace = true;
            missingHands.SetActive(true);
            moveUp.SetBool("handsInPlace", true);
        }

        if (CompareTag("buttons"))
        {
            thisObject.PlayOneShot(Interact.pushAudio);
            int index = panel.GetComponent<PanelScript>().buttons.IndexOf(gameObject);
            panel.GetComponent<PanelScript>().PushButton(index);
        }

        if (CompareTag("swiper") && ItemCollector.keyCardCollected)
        {
            thisObject.PlayOneShot(Interact.pushAudio);
            doorActivated = true;
        }

        if (CompareTag("doorToScene") && doorActivated)
        {
            thisObject.PlayOneShot(Interact.pushAudio);
            gameObject.GetComponent<DoorToScene>().ChangeScene();
        }

        if (CompareTag("bed"))
        {
            Time.timeScale = 0;
            Time.timeScale = 0;
            player.GetComponent<FirstPersonController>().LockMouse();
            winText.enabled = true;
        }
    }

    public IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

    }
}
