using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour, IArrowHittable
{
    [SerializeField]
    private float forceAmount = 1.0f;
    [SerializeField]
    private Material otherMaterial;

    private MeshRenderer meshRenderer;
    private new Rigidbody rigidbody;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Hit(Arrow arrow)
    {
        ApplyMaterial();
        ApplyForce(arrow.transform.forward);
    }

    private void ApplyMaterial()
    {
        meshRenderer.material = otherMaterial;
    }

    private void ApplyForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * forceAmount);
    }
}
