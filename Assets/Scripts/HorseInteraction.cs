using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HorseInteraction : XRBaseInteractable
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
    private Text uiText;
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

    private bool isOnHorse = false;
    private Coroutine hapticCoroutineInstance;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        // OnHorseGrab();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;

        if (selectAction.action.WasPerformedThisFrame())
        {
            if (isOnHorse) return;
            OnHorseSelect();
        }
    }

    private void OnHorseSelect()
    {
        uiText.text = "Voulez-vous faire un tour de joute ?";
        confirmationUI.SetActive(true);
    }

    public void OnInteractionDeclined()
    {
        postProcessVolume.SetActive(false);
        teleportationManager.enabled = true;

        CloseUI();

        if (!isOnHorse) return;

        playerTransform.parent = null;
        playerTransform.position = playerStopPoint.position;
        playerTransform.forward = playerStopPoint.forward;

        isOnHorse = false;

        spear.SetActive(false);

        trainingHorseParent.SetActive(false);
    }

    public void CloseUI()
    {
        confirmationUI.SetActive(false);
    }

    public void OnInteractionConfirmation()
    {
        postProcessVolume.SetActive(true);
        teleportationManager.enabled = false;

        // Place player on top of the horse
        playerTransform.parent = horseTransform;
        playerTransform.forward = horseTransform.forward;
        playerTransform.localPosition = playerPositionOffset;

        playerTransform.GetComponentInChildren<LocomotionSystem>().enabled = false;

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
        uiText.text = "Voulez-vous refaire un tour ?";
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
