using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset actionAsset;
    [SerializeField]
    private XRRayInteractor xrRayInteractor;
    [SerializeField]
    private TeleportationProvider teleportationProvider;

    private InputAction thumbstick;
    private bool isActive;

    private void Start()
    {
        xrRayInteractor.enabled = false;

        InputAction activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        InputAction cancel = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI RightHand").FindAction("Move");
        thumbstick.Enable();
    }

    private void Update()
    {
        if (!isActive) return;

        if (thumbstick.triggered) return;

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

    private void OnTeleportActivate(InputAction.CallbackContext callbackContext)
    {
        xrRayInteractor.enabled = true;
        isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext callbackContext)
    {
        xrRayInteractor.enabled = false;
        isActive = false;
    }
}
