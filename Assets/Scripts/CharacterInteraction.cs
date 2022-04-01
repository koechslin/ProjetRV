using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterInteraction : XRBaseInteractable
{
    [SerializeField]
    private GameObject characterPanel;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        characterPanel.SetActive(true);
    }

    public void OnClose()
    {
        characterPanel.SetActive(false);
    }
}
