using UnityEngine;

public class DeccelerationZone : MonoBehaviour
{
    [SerializeField]
    private HorseControl horseControl;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Horse")) return;

        if (horseControl.isDeccelerating) return;

        StartCoroutine(horseControl.DeccelerationCoroutine());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Horse")) return;

        horseControl.isDeccelerating = false;
    }
}
