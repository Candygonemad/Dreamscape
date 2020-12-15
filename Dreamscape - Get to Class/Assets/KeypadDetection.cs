using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDetection : MonoBehaviour {

    // The objects that sent info
    // to the "BothSeen" method
    private static GameObject sender1;
    private static GameObject sender2;

    // Whether sender1 or 2 
    // sees the player object
    private static bool iSee1 = false;
    private static bool iSee2 = false;

    public static bool BothSeen(bool iSee, GameObject sender)
    {
        // Assigning the senders
        // when they first send data
        if (!sender1)
        {
            sender1 = sender;
        }
        else
        {
            sender2 = sender;
        }

        // Testing to see which
        // sender sent data
        if (sender1.Equals(sender))
        {
            iSee1 = iSee;
        }
        else
        {
            iSee2 = iSee;
        }

        return iSee1 || iSee2;
    }
}
