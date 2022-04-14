using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionVibration : MonoBehaviour
{
    private XRBaseController xr;
    private bool isGrab;
 
    void Start()
    {
        xr = (XRBaseController) GameObject.FindObjectOfType(typeof(XRBaseController));
    }

    public void SetIsGrab(bool boolean)
    {
        isGrab = boolean;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isGrab)
            xr.SendHapticImpulse(0.7f, 2f);
    }
}
