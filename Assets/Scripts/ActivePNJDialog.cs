using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePNJDialog : MonoBehaviour
{
    public GameObject dialog;

    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog.SetActive(false);
        }
    }
}
