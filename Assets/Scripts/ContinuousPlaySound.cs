using UnityEngine;
using Random = UnityEngine.Random;

public class ContinuousPlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField, Range(0.1f, 0.5f)]
    private float volumeChangeMultiplier;
    [SerializeField, Range(0.1f, 0.5f)]
    private float pitchChangeMultiplier;
    [SerializeField]
    private float minDelay;
    [SerializeField]
    private float maxDelay;

    private float nextPlayTimestamp;

    private void Start()
    {
        nextPlayTimestamp = Time.time + Random.Range(minDelay, maxDelay);
    }

    private void Update()
    {
        if (Time.time < nextPlayTimestamp) return;

        audioSource.clip = audioClip;
        audioSource.volume = Random.Range(1 - volumeChangeMultiplier, 1);
        audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        audioSource.PlayOneShot(audioSource.clip);

        nextPlayTimestamp += Random.Range(minDelay, maxDelay);
    }
}
