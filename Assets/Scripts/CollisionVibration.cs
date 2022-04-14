using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionVibration : MonoBehaviour
{
    private XRBaseController xr;
    private bool isGrab;

    [SerializeField] 
    private XRGrabInteractable xrGrab;

    void Start()
    {
        xr = (XRBaseController) GameObject.FindObjectOfType(typeof(XRBaseController));
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(xrGrab.isSelected);

        if(xrGrab.isSelected)
        {
            Debug.Log("Vibration");
            xr.SendHapticImpulse(0.7f, 2f);
        }
    }
}
