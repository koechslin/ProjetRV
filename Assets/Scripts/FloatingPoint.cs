using UnityEngine;

public class FloatingPoint : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
