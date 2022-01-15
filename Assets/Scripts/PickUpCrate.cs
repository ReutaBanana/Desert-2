using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCrate : MonoBehaviour
{
    [SerializeField] Transform dest;
    [SerializeField] GameObject pickUpAlarm;
    private bool isUp = false;
    private int pressCount=1;
    private MovingObstacleLogic script;
    private bool isColided;
    private GameObject alarm;
    [SerializeField] private GameObject openingAlarmParent;
    private GameObject player;
    private CharacterMovement playerMovment;
    private void Start()
    {
        dest = GameObject.Find("Dest").gameObject.transform;
        player = GameObject.Find("Player");
        playerMovment = player.GetComponent<CharacterMovement>();
        openingAlarmParent = GameObject.Find("AlarmParent");
        alarm = Instantiate(pickUpAlarm, openingAlarmParent.transform);
        alarm.SetActive(false);

    }
    private void Update()
    {
        if(isUp)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                pressCount += 1;
                if(pressCount%2==0)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = dest.position;
                    playerMovment.holdingCrate = true;

                    this.transform.parent = GameObject.Find("Dest").transform;
                }
                else
                {
                    this.transform.parent = null;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().isKinematic = false;
                    playerMovment.holdingCrate = false;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
                alarm.SetActive(true);
                        isUp = true;

        }
        else
        {
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        alarm.SetActive(false);
        isUp = false;


        }

    //private void OnCollisionStay(Collision collision)
    //{
       
    //    if (collision.gameObject.CompareTag("Button"))
    //    {
    //        Debug.Log("Button collision");
    //        isColided = true;
    //        script = collision.gameObject.GetComponent<MovingObstacleLogic>();
    //        script.isHit = true;
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Button"))
    //    {
    //        script = collision.gameObject.GetComponent<MovingObstacleLogic>();
    //        script.isHit = false;
    //    }
    //}


}
