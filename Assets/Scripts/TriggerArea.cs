using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerArea : XRBaseInteractor
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
    }
}
