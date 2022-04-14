using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionVibration : MonoBehaviour
{
    private bool isGrab;

    [SerializeField]
    private XRBaseController rHand;
    [SerializeField]
    private XRBaseController lHand;
    [SerializeField] 
    private XRGrabInteractable xrGrab;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(xrGrab.isSelected);

        if(xrGrab.isSelected)
        {
            Debug.Log("Vibration");
            rHand.SendHapticImpulse(0.7f, 2f);
        }
        
        /**
        if(xrGrab.isSelected)
        {
            if ()
            {
                Debug.Log("Right Vibration");
                rHand.SendHapticImpulse(0.7f, 2f);
            }

            else if ()
            {
                Debug.Log("Left Vibration");
                lHand.SendHapticImpulse(0.7f, 2f);
            }
        }*/
    }
}
