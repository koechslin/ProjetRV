using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    [SerializeField]
    private InputActionReference rightGrabReference;
    [SerializeField]
    private InputActionReference leftGrabReference;

    private void Start()
    {
        rightGrabReference.action.performed += OnRightGrab;
        leftGrabReference.action.performed += OnLeftGrab;
    }

    private void OnDestroy()
    {
        rightGrabReference.action.performed -= OnRightGrab;
        leftGrabReference.action.performed -= OnLeftGrab;
    }

    private void OnRightGrab(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Right grab : " + callbackContext);
    }

    private void OnLeftGrab(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Left grab");
    }
}
