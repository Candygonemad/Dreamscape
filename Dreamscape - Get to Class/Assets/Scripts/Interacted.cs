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
    public Image bookPageContainer;
    public Sprite bookPage;
    public ParticleSystem[] candleFlames;
    [HideInInspector]
    public static bool doorActivated;
    private Image[] keyInventoryImageContainers;
    public Sprite thisKeyImage;

    private float smoothness;
    private bool smooth;
    public Material deskCube;

    private bool bookOpened;
    private bool book;

    //AudioSource thisObject;

    public enum UseState
    {
        Gettable, Animatable, Pushable
    };

    public UseState state;
	// Use this for initialization
	void Start () {
        keyInventoryImageContainers = new Image[3];
        GameObject keysUI = GameObject.Find("Keys");
        for (int i = 0; i < keysUI.transform.childCount; i++)
        {
            keyInventoryImageContainers[i] = keysUI.transform.GetChild(i).GetComponent<Image>();
            keysUI.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }

        smooth = true;
        smoothness = 1.0f;
        bookOpened = false;
        bookInPlace = false;
        handsInPlace = false;
        gameObject.AddComponent<AudioSource>();
        //thisObject = gameObject.GetComponent<AudioSource>();
        winText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (deskCube)
        {
            deskCube.SetFloat("_Glossiness", smoothness);
            if (smooth && smoothness < 1.0)
            {
                smoothness += 0.05f;
            }
            else if (!smooth && smoothness > 0.0)
            {
                smoothness -= 0.05f;
            }
            else
            {
                if (smooth)
                    smoothness = 1;
                else
                    smoothness = 0;
            }
        }
    }


    public void GetInteracted()
    {
        
        //Tests if the book is the object being interacted with
        if(CompareTag("book"))
        {
            ItemCollector.bookCollected = true;
        }

        //Test if the hands are the object being interacted with
        if (CompareTag("clock hands"))
        {
            ItemCollector.handsCollected = true;
            Debug.Log(ItemCollector.handsCollected);
        }

        if (CompareTag("key"))
        {
            keyInventoryImageContainers[ItemCollector.keysFound].enabled = true;
            keyInventoryImageContainers[ItemCollector.keysFound].sprite = thisKeyImage;
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
            StartCoroutine(WaitSeconds());
        }

        if(CompareTag("finalDoor") && ItemCollector.keysFound == 3)
        {
            doorSwing.SetBool("allKeys", true);
        }

        if (CompareTag("DeskCube"))
        {
            if (smooth)
                smooth = false;
            else
                smooth = true;
        }

        if (CompareTag("Candles"))
        {
            for (int i = 0; i < candleFlames.Length; i++)
            {
                if (candleFlames[i].enableEmission)
                    candleFlames[i].enableEmission = false;
                else
                    candleFlames[i].enableEmission = true;
            }
        }

        Debug.Log("Animated");
    }

    public void Push()
    {
        //Debug.Log("Pushed");

        //Tests if an openable book is the object being interacted with
        if (CompareTag("openableBook"))
        {
            bookOpened = !bookOpened;
            if(bookOpened)
            {
                bookPageContainer.gameObject.SetActive(true);
                bookPageContainer.sprite = bookPage;

                bookOpened = true;
                Time.timeScale = 0;
                player.GetComponent<FirstPersonController>().LockMouse();
            }
            else
            {
                Time.timeScale = 1;
                player.GetComponent<FirstPersonController>().UnlockMouse();
                bookPageContainer.gameObject.SetActive(false);
                bookOpened = false;
            }
            // display the page to the image display
            
        }

        if (CompareTag("desk"))
        {
            // if the spotlight is off
            bool interactable = !transform.GetChild(DeskPuzzle.spotLightIndex).gameObject.active;
            if (interactable)
            {
                DeskPuzzle.ToggleLights(DeskPuzzle.desks.IndexOf(gameObject));
            }
        }

        if (CompareTag("desk reset"))
        {
            DeskPuzzle.Reset();
        }

        //Tests if the clock is being interacted with and the hands are collected
        if(CompareTag("grandfather clock") && ItemCollector.handsCollected == true)
        {
            handsInPlace = true;
            missingHands.SetActive(true);
            moveUp.SetBool("handsInPlace", true);
        }

        if (CompareTag("buttons"))
        {
            int index = panel.GetComponent<PanelScript>().buttons.IndexOf(gameObject);
            panel.GetComponent<PanelScript>().PushButton(index);
        }

        if (CompareTag("swiper") && ItemCollector.keyCardCollected)
        {
            doorActivated = true;
        }

        if (CompareTag("doorToScene") && doorActivated)
        {
            gameObject.GetComponent<DoorToScene>().ChangeScene();
        }

        if (CompareTag("bed"))
        {
            Time.timeScale = 0;
            Time.timeScale = 0;
            player.GetComponent<FirstPersonController>().LockMouse();
            winText.enabled = true;
            StartCoroutine(WaitSeconds());
            Application.Quit();
        }
    }

    public IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

    }
}
