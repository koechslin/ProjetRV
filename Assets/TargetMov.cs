using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMov : MonoBehaviour
{
    public float point1;
    public float point2;
    public float speed;
    private bool goRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goRight)
        {
            transform.Translate(new Vector3(1,0,0) * speed);
            if (transform.position.x <= point1)
            {
                goRight = false;
            }
        }
        else
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed);
            if (transform.position.x >= point2)
            {
                goRight = true;
            }
        }
    }
}
