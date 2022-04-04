using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    private CharacterInteraction characterInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;

        characterInteraction.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;

        characterInteraction.enabled = false;
    }
}
