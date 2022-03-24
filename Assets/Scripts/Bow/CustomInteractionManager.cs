using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractionManager : XRInteractionManager
{
    public void ForceDeselect(IXRSelectInteractor interactor)
    {
        if (interactor.interactablesSelected.Count > 0)
        {
            SelectExit(interactor, interactor.firstInteractableSelected);
        }
    }
}
