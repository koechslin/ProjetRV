using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillSound : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
}
