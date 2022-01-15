using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunsetTrigger : MonoBehaviour
{
    public bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            isTrigger = true;
        }
    }

}
