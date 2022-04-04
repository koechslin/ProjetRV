using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CharacterInteraction : XRBaseInteractable
{
    [SerializeField]
    private GameObject characterPanel;
    [SerializeField]
    private InputActionReference selectAction;

    private bool isNear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;

        isNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;

        isNear = false;
    }

    private void Update()
    {
        if (isNear && isHovered && selectAction.action.WasPerformedThisFrame())
        {
            characterPanel.SetActive(true);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        // characterPanel.SetActive(true);
    }

    public void OnClose()
    {
        characterPanel.SetActive(false);
    }
}
