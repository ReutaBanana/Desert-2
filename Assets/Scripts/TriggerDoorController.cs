using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor;

    public bool isTrigger = false;  
    [SerializeField] bool isFront;
    public bool isOpen = false;
    private void Update()
    {
        if (isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(!isOpen)
                {
                    isOpen = true;
                    myDoor.SetBool("isOpen", isOpen);
                }
                else
                {
                    isOpen = false;
                    myDoor.SetBool("isOpen", isOpen);
                }
                isTrigger = false;
                
            }
        }
    }
    public void ToggleDoor()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
               isTrigger = true;
            myDoor.SetBool("isFront", isFront);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;

    }



}
