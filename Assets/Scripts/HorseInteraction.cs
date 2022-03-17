using System;
using UnityEngine;
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

    private bool isOnHorse = false;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        OnHorseGrab();
    }

    private void OnHorseGrab()
    {
        confirmationUI.SetActive(true);
    }

    public void CloseUI()
    {
        confirmationUI.SetActive(false);
    }

    public void OnInteractionConfirmation()
    {
        if (isOnHorse) return;

        // Place player on top of the horse
        playerTransform.parent = horseTransform;
        playerTransform.forward = horseTransform.forward;
        playerTransform.localPosition = playerPositionOffset;

        horseControl.enabled = true;
        isOnHorse = true;
        CloseUI();
    }

    public void OnHorseEnd()
    {
        if (!isOnHorse) return;

        playerTransform.parent = null;
        playerTransform.position = playerStopPoint.position;
        playerTransform.forward = playerStopPoint.forward;

        horseControl.enabled = false;
        isOnHorse = false;
    }
}
