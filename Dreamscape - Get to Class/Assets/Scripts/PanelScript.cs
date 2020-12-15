using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> buttons = new List<GameObject>();
    public int buttonAmount;
    [HideInInspector]
    public int[] combination;
    private int[] currentTyped;
    [HideInInspector]
    public GameObject scriptRunner;
    private int correctNumbers;
    private int typedNums;

    public Text keypadText;

    private int keyPadDisplayPosition;
    private string keypadDisplay;
    public Animator drawer;
    public Animator panel;

    public string combinationSet;

    // Use this for initialization
    void Start () {
        // Keypad text should not be visible before
        // something is typed on a keypad
        keypadText.text = null;
        keyPadDisplayPosition = 0;
        combination = new int[combinationSet.Length];
        keypadDisplay = "";
        for (int i = 0; i < combinationSet.Length; i++)
        {
            combination[i] = int.Parse(combinationSet.Substring(i, 1));
        }

        currentTyped = new int[combination.Length];
        typedNums = 0;
		for (int i = 0; i < buttonAmount; i++)
        {
            buttons.Add(transform.GetChild(i).gameObject);
        }
        scriptRunner = gameObject;
        correctNumbers = 0;
	}

    private void Update()
    {
        keypadText.text = keypadDisplay.PadRight(combinationSet.Length, '0');
    }

    public void PushButton(int index)
    {
        int value = int.Parse(buttons[index].gameObject.name);
        currentTyped[typedNums] = value;
        keypadDisplay += currentTyped[typedNums];
        typedNums++;

        if (currentTyped[typedNums - 1] == combination[typedNums - 1])
        {
            correctNumbers++; 
        }

        string debug = "CurrentTyped: ";
        for (int i = 0; i < currentTyped.Length; i++)
        {
            debug += currentTyped[i] + " ";
        }

        Debug.Log(debug);

        Debug.Log("Correct numbers: " + correctNumbers);

        if (correctNumbers == combination.Length && typedNums == combination.Length)
        {
            // REVEAL THE  KEY
            Debug.Log("Correct!");
            keypadText.text = keypadDisplay;
            currentTyped = new int[combination.Length];
            typedNums = 0;
            correctNumbers = 0;
            // ROTATE HERE
            if (drawer)
            {
                drawer.SetBool("puzzleSolved", true);
            }
            if (panel)
            {
                panel.SetBool("combination", true);
            }

            // NOT HERE
            scriptRunner.GetComponent<PanelScript>().enabled = false;
        }
        else if (typedNums == combination.Length)
        {
            currentTyped = new int[combination.Length];
            keypadDisplay = "";
            typedNums = 0;
            Debug.Log("Count : " + typedNums);
            correctNumbers = 0;
        }

    }
}
