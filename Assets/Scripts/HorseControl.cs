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
    
    private Transform targetLocation;
    private int spotIndex;
    private Transform center;
    private float timeCounter = 0f;
    private float timeCounterOffset = 0f;
    private float distance = 0.55f;
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
            halfTurn(transform);
            halfTurn(player);
        }
        else if (spotIndex == 3)
        {
            center = centerSpots[1];
            halfTurn(transform);
            halfTurn(player);
        }
        else
        {
            linearMovement(transform);
            linearMovement(player);
        }
            

        if ((transform.position - targetLocation.transform.position).magnitude <= 0.02f)
        {
            spotIndex++;

            if (spotIndex > 3)
                spotIndex = 0;

            targetLocation = spots[spotIndex];
        }
    }

    private void linearMovement(Transform toMove)
    {
        toMove.position = Vector3.MoveTowards(toMove.position, targetLocation.position, horseSpeed * Time.deltaTime);
    }

    private void halfTurn(Transform toMove) {
        timeCounter += Time.deltaTime * horseSpeed;

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
