using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference activateActionReference;
    [SerializeField]
    private InputActionReference cancelActionReference;
    [SerializeField]
    private InputActionReference confirmTeleportActionReference;
    // [SerializeField]
    // private InputActionReference thumbstickActionReference;
    [SerializeField]
    private XRRayInteractor xrRayInteractor;
    [SerializeField]
    private TeleportationProvider teleportationProvider;


    private InputAction thumbstick;
    private bool isActive;
    private bool buttonReleased = true;

    private void Start()
    {
        xrRayInteractor.enabled = false;

        InputAction activate = activateActionReference.action;
        activate.Enable();
        activate.performed += OnTeleportActivatePerformed;
        activate.canceled += OnTeleportActivateCanceled;

        InputAction cancel = cancelActionReference.action;
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        // thumbstick = thumbstickActionReference.action;
        // thumbstick.Enable();

        confirmTeleportActionReference.action.performed += ConfirmTeleport;
    }

    private void ConfirmTeleport(InputAction.CallbackContext callbackContext)
    {
        if (!isActive || !buttonReleased) return;

        buttonReleased = false;

        if (!xrRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            xrRayInteractor.enabled = false;
            isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            // destinationRotation = ?,
        };

        teleportationProvider.QueueTeleportRequest(request);

        xrRayInteractor.enabled = false;
        isActive = false;
    }

    private void OnTeleportActivatePerformed(InputAction.CallbackContext callbackContext)
    {
        if (isActive || !buttonReleased) return;

        buttonReleased = false;
        xrRayInteractor.enabled = true;
        isActive = true;
    }

    private void OnTeleportActivateCanceled(InputAction.CallbackContext callbackContext)
    {
        buttonReleased = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext callbackContext)
    {
        xrRayInteractor.enabled = false;
        isActive = false;
    }
}
