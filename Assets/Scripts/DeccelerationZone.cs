using UnityEngine;

public class DeccelerationZone : MonoBehaviour
{
    [SerializeField]
    private HorseControl playerHorseControl;
    [SerializeField]
    private HorseControl trainingHorseControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Horse") && !playerHorseControl.isDeccelerating) StartCoroutine(playerHorseControl.DeccelerationCoroutine());

        if (other.CompareTag("TrainingHorse") && !trainingHorseControl.isDeccelerating) StartCoroutine(trainingHorseControl.DeccelerationCoroutine());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Horse")) playerHorseControl.isDeccelerating = false;

        if (other.CompareTag("TrainingHorse")) trainingHorseControl.isDeccelerating = false;
    }
}
