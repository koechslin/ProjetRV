using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CharacterInteraction : XRBaseInteractable
{
    [SerializeField]
    private GameObject characterPanel;
    [SerializeField]
    private InputActionReference selectAction;

    private void Update()
    {
        if (isHovered && selectAction.action.WasPerformedThisFrame())
        {
            characterPanel.SetActive(true);
        }
    }

    public void OnClose()
    {
        characterPanel.SetActive(false);
    }
}
