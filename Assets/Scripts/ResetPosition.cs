using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform basePosition;
    [SerializeField]
    private Transform target;

    public void Reset()
    {
        target.position = basePosition.position;
        target.rotation = basePosition.rotation;
    }
}
