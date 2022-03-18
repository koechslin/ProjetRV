using System;
using UnityEngine;
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

    private bool isOnHorse = false;

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
        // Place player on top of the horse
        playerTransform.parent = horseTransform;
        playerTransform.forward = horseTransform.forward;
        playerTransform.localPosition = playerPositionOffset;

        playerTransform.GetComponentInChildren<LocomotionSystem>().enabled = false;

        horseControl.enabled = true;
        isOnHorse = true;
        CloseUI();
    }

    public void OnHorseEnd()
    {
        if (!isOnHorse) return;

        // Stop horse
        horseControl.enabled = false;

        // Prompt player for another lap
        uiText.text = "Voulez-vous refaire un tour ?";
        confirmationUI.SetActive(true);
    }
}
