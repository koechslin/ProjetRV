using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DetectHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Hittable hittable = collision.gameObject.GetComponentInChildren<Hittable>();

        if (hittable == null) return;

        hittable.Hit();
    }
}
