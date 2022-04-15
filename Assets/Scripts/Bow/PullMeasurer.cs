using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PullMeasurer : XRBaseInteractable
{
    public float PullAmount { get; private set; } = 0.0f;

    public class PullEvent : UnityEvent<Vector3, float>
    {
    };
    public PullEvent pulled = new PullEvent();

    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;

    private IXRSelectInteractor pullingInteractor = null;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Set interactor for measurement
        pullingInteractor = args.interactorObject;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Clear interactor and reset pull amount for animation
        pullingInteractor = null;
        SetPullValues(start.position, 0.0f);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected) return;

        // Update pull values while the measurer is grabbed
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            CheckForPull();
        }
    }

    private void CheckForPull()
    {
        // Use the interactor's position to calculate amount
        Vector3 interactorPosition = pullingInteractor.transform.position;

        // Figure out the new pull value and its position in space
        float newPullAmount = CalculatePull(interactorPosition);
        Vector3 newPullPosition = CalculatePosition(newPullAmount);

        SetPullValues(newPullPosition, newPullAmount);
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        // Direction and length

        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;

        float maxLength = targetDirection.magnitude;
        targetDirection.Normalize();

        // What's the actual distance ?
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        pullValue = Mathf.Clamp(pullValue, 0.0f, 1.0f);

        return pullValue;
    }

    private Vector3 CalculatePosition(float amount)
    {
        // Find the actual position of the hand
        return Vector3.Lerp(start.position, end.position, amount);
    }

    private void SetPullValues(Vector3 newPullPosition, float newPullAmount)
    {
        // If it's a new value
        if (Math.Abs(newPullAmount - PullAmount) < 0.01f) return;

        PullAmount = newPullAmount;
        pulled?.Invoke(newPullPosition, newPullAmount);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        // Only let direct interactors pull the string
        return base.IsSelectableBy(interactor) && IsDirectInteractor(interactor);
    }

    private bool IsDirectInteractor(IXRSelectInteractor interactor)
    {
        return interactor is XRDirectInteractor;
    }

    private void OnDrawGizmos()
    {
        // Draw line from start to end point
        if (start && end)
        {
            Gizmos.DrawLine(start.position, end.position);
        }
    }
}
