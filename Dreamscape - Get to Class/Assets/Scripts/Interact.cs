using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

    public Camera cam;
    private Ray ray;
    private RaycastHit hit;
    public LayerMask layerMask;
    public GameObject interactHit;
    public Text interactionText;

    public AudioSource getAudio;
    public AudioSource pushAudio;
    public AudioSource animateKeyAudio;

    public Image bookPageContainer;

    [HideInInspector]
    public Camera playerCamera;

    // Use this for initialization
    void Start () {
        if (bookPageContainer)
            bookPageContainer.gameObject.SetActive(false);
        layerMask = LayerMask.GetMask("Interactable");
        interactionText.enabled = false;
        playerCamera = GetComponentInChildren<Camera>();
        playerCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("DisabledLayer"));
    }
	
	// Update is called once per frame
	void Update () {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction , Color.red);
        if (Physics.Raycast(ray, out hit, 1.5f, layerMask.value))
        {
            interactionText.enabled = true;
            //Debug.Log("here");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bookPageContainer)
                    bookPageContainer.gameObject.SetActive(false);

                //Debug.Log("key down");
                interactHit = hit.transform.gameObject;
                int useState = (int)(interactHit.GetComponent<Interacted>().state);

                if (useState == 0)
                {
                    if (!interactHit.CompareTag("key"))
                        getAudio.Play();
                    else
                        animateKeyAudio.Play();
                        interactHit.GetComponent<Interacted>().GetInteracted();
                }
                else if (useState == 1)
                {
                    interactHit.GetComponent<Interacted>().Animate();
                }
                if (useState == 2)
                {
                    pushAudio.Play();
                    interactHit.GetComponent<Interacted>().Push();
                }
            }
        }
        else
            interactionText.enabled = false;
        
	}
}
