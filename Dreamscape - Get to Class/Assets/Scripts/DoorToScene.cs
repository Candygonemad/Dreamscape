using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToScene : MonoBehaviour {

	public void ChangeScene()
    {
        Debug.Log("Teleporting");
        SceneManager.LoadScene("SampleScene");
    }
}
