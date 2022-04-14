using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HittableCharacter : Hittable
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hitAudioClip;
    [SerializeField]
    private float hapticAmplitude;
    [SerializeField]
    private float hapticDuration;

    private ActionBasedController[] controllers;

    private void Start()
    {
        controllers = FindObjectsOfType<ActionBasedController>();
    }

    public override void Hit()
    {
        audioSource.PlayOneShot(hitAudioClip);

        foreach (ActionBasedController xrController in controllers)
        {
            xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }
    }
}
