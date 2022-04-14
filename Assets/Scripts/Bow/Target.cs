using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour, IArrowHittable
{
    [SerializeField]
    private float forceAmount = 1.0f;
    [SerializeField]
    private Transform targetCenter;
    [SerializeField]
    private Transform[] limitsPoints; // 10, 9, ..., 0
    [SerializeField]
    private Transform[] projectionPoints;
    [SerializeField]
    private GameObject floatingPointPrefab;
    [SerializeField]
    private Transform floatingPointSpawnPoint;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip arrowHitAudioClip;

    private new Rigidbody rigidbody;
    private float[] distances;
    private Vector3 projectionVector1;
    private Vector3 projectionVector2;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        CalculateDistanceBetweenCenterAndLimits();
        projectionVector1 = projectionPoints[0].position - targetCenter.position;
        projectionVector2 = projectionPoints[1].position - targetCenter.position;
    }

    public void Hit(Arrow arrow, Vector3 hitPoint)
    {
        audioSource.PlayOneShot(arrowHitAudioClip);

        ApplyForce(arrow.transform.forward);
        DisplayPoints(hitPoint);
    }

    private void ApplyForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * forceAmount);
    }

    private void CalculateDistanceBetweenCenterAndLimits()
    {
        distances = new float[11];

        for (int i = 0; i < 11; ++i)
        {
            distances[i] = (targetCenter.position - limitsPoints[i].position).magnitude;
        }
    }

    private void DisplayPoints(Vector3 hitPoint)
    {
        Vector3 vectorCenterToHitPoint = hitPoint - targetCenter.position;
        float projection1 = Mathf.Abs(Vector3.Dot(vectorCenterToHitPoint, projectionVector1));
        float projection2 = Mathf.Abs(Vector3.Dot(vectorCenterToHitPoint, projectionVector2));

        float distanceFromCenter = (projection1 * projectionVector1 + projection2 * projectionVector2).magnitude;

        for (int i = 0; i < 11; ++i)
        {
            if (!(distanceFromCenter <= distances[i])) continue;

            GameObject floatingPoint = Instantiate(floatingPointPrefab, floatingPointSpawnPoint.position, floatingPointSpawnPoint.rotation);

            TextMeshPro floatingPointText = floatingPoint.GetComponentInChildren<TextMeshPro>();
            floatingPointText.text = (10 - i) + " points !";

            break;
        }
    }
}
