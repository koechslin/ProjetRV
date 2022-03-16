using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseControl : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform[] spots;
    [SerializeField]
    private Transform[] centerSpots;
    [SerializeField]
    private float horseSpeed = 0.1f;
    [SerializeField]
    private Transform horseParent;
    [SerializeField]
    private float halfTurnSpeedFactor;
    
    private Transform targetLocation;
    private int spotIndex;
    private Transform center;
    private float timeCounter = 0f;
    private float timeCounterOffset = Mathf.PI / 2.0f;
    private bool isMoving = false;

    void Start()
    {
        spotIndex = 0;
        targetLocation = spots[spotIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (spotIndex == 1) 
        {
            center = centerSpots[0];
            halfTurn(horseParent);
            halfTurn(player);
        }
        else if (spotIndex == 3)
        {
            center = centerSpots[1];
            halfTurn(horseParent);
            halfTurn(player);
        }
        else
        {
            linearMovement(horseParent);
            linearMovement(player);
        }
            

        if ((horseParent.position - targetLocation.transform.position).magnitude <= 0.05f)
        {
            spotIndex++;

            if (spotIndex > 3)
                spotIndex = 0;

            targetLocation = spots[spotIndex];
        }
    }

    private void linearMovement(Transform toMove)
    {
        float y = toMove.position.y;
        Vector3 newPosition = Vector3.MoveTowards(toMove.position, targetLocation.position, horseSpeed * Time.deltaTime);
        newPosition.y = y;

        toMove.position = newPosition;
    }

    private void halfTurn(Transform toMove)
    {
        Vector3 positionDiff = (toMove.transform.position - center.position);
        positionDiff.y = 0.0f;
        float distance = positionDiff.magnitude;

        // distance = 3.0f;

        timeCounter += Time.deltaTime * horseSpeed * halfTurnSpeedFactor;

        float x = center.position.x + Mathf.Cos(timeCounter + timeCounterOffset) * distance;
        float z = center.position.z + Mathf.Sin(timeCounter + timeCounterOffset) * distance;

        toMove.position = new Vector3(x, toMove.position.y, z);
                    
        toMove.forward = new Vector3(-Mathf.Sin(timeCounter + timeCounterOffset), toMove.forward.y, Mathf.Cos(timeCounter + timeCounterOffset));

        /*if (timeCounter >= Mathf.PI)
        {
                isMoving = false;
        }*/
    }
}
