using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HorseInteraction : XRBaseInteractable
{
    [SerializeField]
    private HorseControl horseControl;
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
    private Animator animator;
    [SerializeField]
    private PostProcessLayer postProcessLayer;

    private bool isOnHorse = false;
    private Coroutine hapticCoroutineInstance;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        OnHorseGrab();
    }

    private void OnHorseGrab()
    {
        uiText.text = "Voulez-vous faire un tour de joute ?";
        confirmationUI.SetActive(true);
    }

    public void OnInteractionDeclined()
    {
        postProcessLayer.enabled = false;

        CloseUI();

        if (!isOnHorse) return;

        playerTransform.parent = null;
        playerTransform.position = playerStopPoint.position;
        playerTransform.forward = playerStopPoint.forward;

        isOnHorse = false;
    }

    public void CloseUI()
    {
        confirmationUI.SetActive(false);
    }

    public void OnInteractionConfirmation()
    {
        postProcessLayer.enabled = true;

        // Place player on top of the horse
        playerTransform.parent = horseTransform;
        playerTransform.forward = horseTransform.forward;
        playerTransform.localPosition = playerPositionOffset;

        playerTransform.GetComponentInChildren<LocomotionSystem>().enabled = false;

        horseControl.enabled = true;
        isOnHorse = true;
        CloseUI();

        // Haptic
        // hapticCoroutineInstance = StartCoroutine("HapticCoroutine");
        //Start running animation
        animator.SetBool("IsRunning", true);
    }

    public void OnHorseEnd()
    {
        if (!isOnHorse) return;

        // Stop horse
        horseControl.enabled = false;

        // Stop haptic
        // StopCoroutine(hapticCoroutineInstance);
        //Stop running animation
        animator.SetBool("IsRunning", false);

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
            yield return null;
        }
    }
}
