using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour {

    public List<GameObject> buttons = new List<GameObject>();
    public int buttonAmount;
    public int[] combination;
    private int[] currentTyped;
    public GameObject scriptRunner;
    private int correctNumbers;
    private int typedNums;

    public Animator drawer;
    public Animator panel;

    public string combinationSet;

    // Use this for initialization
    void Start () {
        combination = new int[combinationSet.Length];
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

    public void PushButton(int index)
    {
        int value = int.Parse(buttons[index].gameObject.name);
        currentTyped[typedNums] = value;
        typedNums++;

        if (currentTyped[typedNums - 1] == combination[typedNums - 1])
        {
            correctNumbers++; 
        }

        //if (currentTyped.Count == combination.Length)
        //{
        //    for (int i = 0; i < combination.Length; i++)
        //    {
        //        if (currentTyped[i] == combination[i])
        //        {
        //            correctNumbers++;
        //        }
        //    }
        //}

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
            typedNums = 0;
            Debug.Log("Count : " + typedNums);
            correctNumbers = 0;
        }

    }
}
