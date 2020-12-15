using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskPuzzle : MonoBehaviour {
    // All the student desks in the lightPuzzle
    public static List<GameObject> desks = new List<GameObject>(9);

    public GameObject desk1;
    public GameObject desk2;
    public GameObject desk3;
    public GameObject desk4;
    public GameObject desk5;
    public GameObject desk6;
    public GameObject desk7;
    public GameObject desk8;
    public GameObject desk9;
    public static GameObject key;
    public GameObject keyInitializer;
    private static int lightsOn;

    public static Animator keyFall;
    public Animator keyFallInitializer;

    public static bool puzzleStarted = false;
    public const int spotLightIndex = 2;

    public static GameObject scriptRunner;

    // Use this for initialization
    void Start () {
        key = keyInitializer;
        keyFall = keyFallInitializer;
        scriptRunner = gameObject;
        // Populating the desks list
        desks.Add(desk1);
        desks.Add(desk2);
        desks.Add(desk3);
        desks.Add(desk4);
        desks.Add(desk5);
        desks.Add(desk6);
        desks.Add(desk7);
        desks.Add(desk8);
        desks.Add(desk9);

        // Middle desk starts out off
        desks[4].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
    }

    public static void PuzzleStart()
    {
        for (int i = 0; i < desks.Count; i++)
        {
            // Setting all the spotlights off
            desks[i].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
        }
        desks[4].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
        puzzleStarted = true;
    }

    public static void Reset()
    {
        if (lightsOn != 9)
        {
            for (int i = 0; i < desks.Count; i++)
            {
                if (i == 4)
                {
                    desks[i].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
                }
                else
                {
                    desks[i].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
                }
            }
        }
    }

    public static void ToggleLights(int index)
    {
        int up = index - 3;
        int down = index + 3;
        int left = index - 1;
        int right = index + 1;

        // Only possible left positions on a 3x3 grid are
        // positions 0, 3, and 6, and 2, 5, and 8 for right
        // positions
        bool canLeft = !(index == 0 || index == 3 || index == 6);
        bool canRight = !(index == 2 || index == 5 || index == 8);

        bool canUp = up > -1;
        bool canDown = down < 9;

        desks[index].transform.GetChild(spotLightIndex).gameObject.SetActive(true);

        if (canUp)
        {
            bool upLight = desks[up].transform.GetChild(spotLightIndex).gameObject.active;
            if (upLight)
            {
                desks[up].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
            }
            else
            {
                desks[up].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
            }
        }
        if (canDown)
        {
            bool downLight = desks[down].transform.GetChild(spotLightIndex).gameObject.active;
            if (downLight)
            {
                desks[down].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
            }
            else
            {
                desks[down].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
            }
        }
        if (canLeft)
        {
            bool leftLight = desks[left].transform.GetChild(spotLightIndex).gameObject.active;
            if (leftLight)
            {
                desks[left].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
            }
            else
            {
                desks[left].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
            }
        }
        if (canRight)
        {
            bool rightLight = desks[right].transform.GetChild(spotLightIndex).gameObject.active;
            if (rightLight)
            {
                desks[right].transform.GetChild(spotLightIndex).gameObject.SetActive(false);
            }
            else
            {
                desks[right].transform.GetChild(spotLightIndex).gameObject.SetActive(true);
            }
        }

        lightsOn = 0;
        for (int i = 0; i < desks.Count; i++)
        {
            if (desks[i].transform.GetChild(spotLightIndex).gameObject.active)
            {
                lightsOn++;
            }
        }

        if (lightsOn == 9)
        {
            Debug.Log("You win the light puzzle");
            key.SetActive(true);
            keyFall.SetBool("puzzleComplete", true);
            scriptRunner.SetActive(false);
        }
    }
}
