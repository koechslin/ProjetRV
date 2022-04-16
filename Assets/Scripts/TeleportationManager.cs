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
    [SerializeField]
    private GameObject teleportReticle;


    private InputAction thumbstick;
    private bool isActive;
    private bool buttonReleased = true;

    private void Start()
    {
        DeactivateRay();
    }

    private void OnEnable()
    {
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

    private void OnDisable()
    {
        InputAction activate = activateActionReference.action;
        activate.performed -= OnTeleportActivatePerformed;
        activate.canceled -= OnTeleportActivateCanceled;

        InputAction cancel = cancelActionReference.action;
        cancel.performed -= OnTeleportCancel;

        // thumbstick = thumbstickActionReference.action;
        // thumbstick.Enable();

        confirmTeleportActionReference.action.performed -= ConfirmTeleport;
    }

    private void ConfirmTeleport(InputAction.CallbackContext callbackContext)
    {
        if (!isActive || !buttonReleased) return;

        buttonReleased = false;

        if (!xrRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) )
        {
            DeactivateRay();
            return;
        }

        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Teleport")) return;

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            // destinationRotation = ?,
        };

        teleportationProvider.QueueTeleportRequest(request);

        DeactivateRay();
    }

    private void OnTeleportActivatePerformed(InputAction.CallbackContext callbackContext)
    {
        if (isActive || !buttonReleased) return;

        buttonReleased = false;
        ActivateRay();
    }

    private void OnTeleportActivateCanceled(InputAction.CallbackContext callbackContext)
    {
        buttonReleased = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext callbackContext)
    {
        DeactivateRay();
    }

    public void ActivateRay()
    {
        xrRayInteractor.enabled = true;
        isActive = true;
        teleportReticle.SetActive(true);
    }

    public void DeactivateRay()
    {
        xrRayInteractor.enabled = false;
        isActive = false;
        teleportReticle.SetActive(false);
    }
}
