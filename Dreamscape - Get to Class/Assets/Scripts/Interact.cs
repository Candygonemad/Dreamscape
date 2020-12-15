using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

    public Camera cam;
    private Ray ray;
    private RaycastHit hit;
    private LayerMask layerMask;
    private GameObject interactHit;
    public Text interactionText;

    public static AudioClip getAudio;
    public static AudioClip pushAudio;
    public static AudioClip animateKeyAudio;

    public AudioClip get;
    public AudioClip push;
    public AudioClip animatekey;

    public AudioSource player;

    public Camera playerCamera;

    // Use this for initialization
    void Start () {
        getAudio = get;
        pushAudio = push;
        animateKeyAudio = animatekey;
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
                //Debug.Log("key down");
                interactHit = hit.transform.gameObject;
                int useState = (int)(interactHit.GetComponent<Interacted>().state);

                interactHit.AddComponent<AudioSource>();
                player = interactHit.GetComponent<AudioSource>();

                if (useState == 0)
                {
                    if (!interactHit.CompareTag("key")) player.PlayOneShot(getAudio);
                    else player.PlayOneShot(animateKeyAudio);
                    interactHit.GetComponent<Interacted>().GetInteracted();
                }
                else if (useState == 1)
                {
                    interactHit.GetComponent<Interacted>().Animate();
                }
                if (useState == 2)
                {
                    interactHit.GetComponent<Interacted>().Push();
                }
            }
        }
        else
            interactionText.enabled = false;
        
	}
}
