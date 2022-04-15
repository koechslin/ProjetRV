using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Arrow : XRGrabInteractable
{
    [Header("Settings")]
    [SerializeField]
    private float speed = 2000.0f;

    [Header("Hit")]
    [SerializeField]
    private Transform tip;
    [SerializeField]
    private LayerMask layerMask = ~Physics.IgnoreRaycastLayer;

    private new Collider collider;
    private new Rigidbody rigidbody;
    private Vector3 lastPosition = Vector3.zero;
    private bool launched = false;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Do this first so we get the right physics value
        if (args.interactorObject is XRDirectInteractor)
        {
            // Clear(); // ???
        }

        base.OnSelectEntering(args);
    }

    private void Clear()
    {
        SetLaunch(false);
        TogglePhysics(false);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // If it's a notch, launch the arrow
        if (args.interactorObject is Notch notch)
        {
            Launch(notch);
        }
    }

    private void Launch(Notch notch)
    {
        // Double check in case the bow is dropped with arrow socketed
        if (!notch.IsReady) return;

        SetLaunch(true);
        UpdateLastPosition();
        ApplyForce(notch.PullMeasurer);
    }

    private void SetLaunch(bool value)
    {
        collider.isTrigger = value;
        launched = value;
    }

    private void UpdateLastPosition()
    {
        // Always use the tip's position
        lastPosition = tip.position;
    }

    private void ApplyForce(PullMeasurer pullMeasurer)
    {
        // Apply force to the arrow
        float power = pullMeasurer.PullAmount;
        Vector3 force = transform.forward * (power * speed);
        rigidbody.AddForce(force);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!launched) return;

        switch (updatePhase)
        {
            // Check for collision as often as possible
            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
            {
                if (CheckForCollision())
                {
                    launched = false;
                }

                UpdateLastPosition();
                break;
            }

            // Only set the direction with each physics update
            case XRInteractionUpdateOrder.UpdatePhase.Fixed:
                SetDirection();
                break;

            default:
                break;
        }
    }

    private void SetDirection()
    {
        // Look in the direction the arrow is moving
        if (rigidbody.velocity.z > 0.5f)
        {
            transform.forward = rigidbody.velocity;
        }
    }

    private bool CheckForCollision()
    {
        // Check if there was a hit
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hit, layerMask))
        {
            TogglePhysics(false);
            ChildArrow(hit);
            CheckForHittable(hit);
        }

        return hit.collider != null;
    }

    private void TogglePhysics(bool value)
    {
        // Disable physics for childing and grabbing
        rigidbody.isKinematic = !value;
        rigidbody.useGravity = value;
    }

    private void ChildArrow(RaycastHit hit)
    {
        // Child to hit object
        Transform newParent = hit.collider.transform;
        transform.SetParent(newParent);
    }

    private void CheckForHittable(RaycastHit hit)
    {
        // Check if the hit object has a component that uses the hittable interface
        GameObject hitObject = hit.transform.gameObject;
        IArrowHittable hittable = hitObject ? hitObject.GetComponent<IArrowHittable>() : null;

        // If we find a valid component, call whatever functionality it has
        if (hittable != null)
        {
            hittable.Hit(this, hit.point);
        }
    }
}
