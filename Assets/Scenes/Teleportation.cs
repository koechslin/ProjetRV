using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Teleportation : MonoBehaviour
{
    [SerializeField]
    private InputActionReference grip;

    [SerializeField]
    private Vector3[] pointsDeTeleportations;

    private int compteur = 0;

    private void Start()
    {
        InputAction action = grip.action;
        if (action != null)
        {
            action.performed += OnGrip;

        }
    }
    private void OnGrip(InputAction.CallbackContext callbackContext)
    {
        compteur++;
        if (compteur >= pointsDeTeleportations.Length)
        {
            compteur = 0;
        }
        transform.position = pointsDeTeleportations[compteur];

    }
}

