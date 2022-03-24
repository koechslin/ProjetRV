using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable
{
    [SerializeField]
    private GameObject arrowPrefab;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectArrow);
    }

    private void CreateAndSelectArrow(SelectEnterEventArgs args)
    {
        // Create an arrow and force into interacting hand
        Arrow arrow = CreateArrow(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, arrow);
    }

    private Arrow CreateArrow(Transform orientation)
    {
        // Create arrow and get Arrow component
        GameObject arrowObject = Instantiate(arrowPrefab, orientation.position, orientation.rotation);
        return arrowObject.GetComponent<Arrow>();
    }
}
