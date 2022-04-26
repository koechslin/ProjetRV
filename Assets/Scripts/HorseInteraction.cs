using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HorseInteraction : MonoBehaviour
{
    [SerializeField]
    private HorseControl playerHorseControl;
    [SerializeField]
    private HorseControl trainingHorseControl;
    [SerializeField]
    private GameObject confirmationUI;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform horseTransform;
    [SerializeField]
    private Vector3 playerPositionOffset;
    [SerializeField]
    private Transform playerStopPoint;
    [SerializeField]
    private ActionBasedController[] controllers;
    [SerializeField]
    private float hapticAmplitude;
    [SerializeField]
    private float hapticDuration;
    [SerializeField]
    private Animator playerHorseAnimator;
    [SerializeField]
    private Animator trainingHorseAnimator;
    [SerializeField]
    private GameObject postProcessVolume;
    [SerializeField]
    private TeleportationManager teleportationManager;
    [SerializeField]
    private InputActionReference selectAction;
    [SerializeField]
    private float hapticCoef;
    [SerializeField]
    private GameObject spear;
    [SerializeField]
    private GameObject trainingHorseParent;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject pnjPanel;
    [SerializeField]
    private XROrigin xrOrigin;
    [SerializeField]
    private Transform mainCameraTransform;

    private bool isOnHorse = false;
    private Coroutine hapticCoroutineInstance;

    public void OnInteractionDeclined()
    {
        postProcessVolume.SetActive(false);
        teleportationManager.enabled = true;

        CloseUI();

        if (!isOnHorse) return;

        playerTransform.parent = null;
        playerTransform.position = playerStopPoint.position;
        playerTransform.forward = playerStopPoint.forward;

        Vector3 mainCameraForward = mainCameraTransform.forward;
        Vector3 stopPointForward = playerStopPoint.forward;

        float rotation = Mathf.Acos(Vector3.Dot(mainCameraForward, stopPointForward)) * Mathf.Rad2Deg;

        if (!float.IsInfinity(rotation) && !float.IsNaN(rotation)) xrOrigin.RotateAroundCameraPosition(Vector3.up, rotation);

        isOnHorse = false;

        spear.SetActive(false);
        shield.SetActive(false);

        trainingHorseParent.SetActive(false);
    }

    public void CloseUI()
    {
        confirmationUI.SetActive(false);
        pnjPanel.SetActive(false);
    }

    public void OnInteractionConfirmation()
    {
        postProcessVolume.SetActive(true);
        teleportationManager.enabled = false;

        // Place player on top of the horse
        if (!isOnHorse)
        {
            Vector3 mainCameraForward = mainCameraTransform.forward;
            mainCameraForward.y = 0.0f;
            Vector3 horseForward = horseTransform.forward;
            horseForward.y = 0.0f;

            Quaternion tmpQuaternion = Quaternion.FromToRotation(mainCameraForward, horseForward);
            tmpQuaternion.ToAngleAxis(out float angleRotation, out Vector3 axisRotation);
            xrOrigin.RotateAroundCameraPosition(axisRotation, angleRotation);

            playerTransform.parent = horseTransform;
            // playerTransform.forward = horseTransform.forward;
            playerTransform.localPosition = playerPositionOffset;
        }

        playerTransform.GetComponentInChildren<LocomotionSystem>().enabled = false;
        playerTransform.GetComponentInChildren<TeleportationManager>().DeactivateRay();

        trainingHorseParent.SetActive(true);

        playerHorseControl.enabled = true;
        trainingHorseControl.enabled = true;
        isOnHorse = true;
        CloseUI();

        // Haptic
        hapticCoroutineInstance = StartCoroutine("HapticCoroutine");
        //Start running animation
        playerHorseAnimator.SetBool("IsRunning", true);
        trainingHorseAnimator.SetBool("IsRunning", true);

        spear.SetActive(true);
        shield.SetActive(true);
    }

    public void OnHorseEnd()
    {
        if (!isOnHorse) return;

        // Stop horse
        playerHorseControl.enabled = false;
        trainingHorseControl.enabled = false;

        playerHorseAnimator.speed = 1.0f;
        trainingHorseAnimator.speed = 1.0f;

        // Stop haptic
        StopCoroutine(hapticCoroutineInstance);
        //Stop running animation
        playerHorseAnimator.SetBool("IsRunning", false);
        trainingHorseAnimator.SetBool("IsRunning", false);

        // Prompt player for another lap
        confirmationUI.SetActive(true);
    }

    private IEnumerator HapticCoroutine()
    {
        while (true)
        {
            foreach (ActionBasedController xrController in controllers)
            {
                xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
            }
            yield return new WaitForSeconds(hapticCoef * 1.0f / playerHorseControl.GetHorseSpeed());

            foreach (ActionBasedController xrController in controllers)
            {
                xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
            }
            yield return new WaitForSeconds(hapticCoef * 1.0f / playerHorseControl.GetHorseSpeed());

            foreach (ActionBasedController xrController in controllers)
            {
                xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
            }
            // yield return new WaitForSeconds(0.4f);

            yield return new WaitForSeconds(3 * hapticCoef * 1.0f / playerHorseControl.GetHorseSpeed());
        }
    }
}
