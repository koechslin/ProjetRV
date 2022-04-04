using UnityEngine;

public class UIRotateTowardPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position, new Vector3(0.0f, 1.0f, 0.0f));
    }
}
