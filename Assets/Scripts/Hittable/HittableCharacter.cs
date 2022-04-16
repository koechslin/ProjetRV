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
    [SerializeField]
    private ActionBasedController rightHand;

    public override void Hit()
    {
        audioSource.PlayOneShot(hitAudioClip);

        rightHand.SendHapticImpulse(hapticAmplitude, hapticDuration);
    }
}
