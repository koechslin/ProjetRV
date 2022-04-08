using UnityEngine;
using UnityEngine.UI;

public class TargetMov : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed;
    public float epsilonDistance;
    public Toggle toggleMove;
    public Transform defaultPosition;

    private Transform currentPoint;
    private bool isActivated;

    private void Start()
    {
        currentPoint = point1;
        isActivated = false;
    }

    public void OnToggleChanged()
    {
        isActivated = toggleMove.isOn;
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition.position;
    }

    void Update()
    {
        if (!isActivated) return;

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

        if ((currentPoint.position - transform.position).magnitude <= epsilonDistance)
        {
            currentPoint = currentPoint == point1 ? point2 : point1;
        }
    }
}
