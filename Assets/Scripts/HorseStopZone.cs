using UnityEngine;

public class HorseStopZone : MonoBehaviour
{
    [SerializeField]
    private HorseInteraction horseInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Horse")) return;

        horseInteraction.OnHorseEnd();
    }
}
