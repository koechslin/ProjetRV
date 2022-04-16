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
    [SerializeField]
    private float hapticDuration;
    [SerializeField]
    private float hapticAmplitude;

    void OnCollisionStay(Collision collision)
    {
        // Debug.Log(xrGrab.isSelected);

        if(xrGrab.isSelected)
        {
            XRDirectInteractor tmpInteractor = xrGrab.firstInteractorSelecting as XRDirectInteractor;
            XRBaseController tmpController = tmpInteractor.GetComponentInParent<XRBaseController>();
            if (tmpController == rHand) rHand.SendHapticImpulse(hapticAmplitude, hapticDuration);
            else lHand.SendHapticImpulse(hapticAmplitude, hapticDuration);
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
