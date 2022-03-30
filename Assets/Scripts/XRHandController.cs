using UnityEngine;
using UnityEngine.InputSystem;

public class XRHandController : MonoBehaviour
{
    [SerializeField]
    private float thumbMoveSpeed = 0.1f;
    [SerializeField]
    private InputActionReference gripActionReference;
    [SerializeField]
    private InputActionReference triggerActionReference;
    [SerializeField]
    private InputActionReference primaryActionReference;
    [SerializeField]
    private InputActionReference secondaryActionReference;

    private Animator animator;

    private float indexValue;
    private float thumbValue;
    private float threeFingersValue;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateHand();
    }

    private void AnimateHand()
    {
        indexValue = triggerActionReference.action.ReadValue<float>();
        threeFingersValue = gripActionReference.action.ReadValue<float>();

        if (primaryActionReference.action.IsPressed() || secondaryActionReference.action.IsPressed())
        {
            thumbValue += thumbMoveSpeed;
        }
        else
        {
            thumbValue -= thumbMoveSpeed;
        }

        thumbValue = Mathf.Clamp(thumbValue, 0, 1);

        animator.SetFloat("Index", indexValue);
        animator.SetFloat("ThreeFingers", threeFingersValue);
        animator.SetFloat("Thumb", thumbValue);
    }
}
