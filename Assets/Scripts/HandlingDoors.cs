using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingDoors : MonoBehaviour
{
    [SerializeField] private Animator myDoor;

    [SerializeField] private GameObject openingAlarm;
    [SerializeField] private GameObject closingAlarm;
    [SerializeField] private GameObject lockedAlarm;
    [SerializeField] private GameObject unLockedAlarm;
    [SerializeField] private GameObject openingAlarmParent;
    private GameObject alarm;

    [SerializeField] GameObject frontDoorController;
    [SerializeField] GameObject backDoorController;
    private bool frontDoorTrigger;
    private bool backDoorTrigger;

    private bool isOpen=false;
    public bool isLockedDoorPuzzle1;
    public bool isLockedDoorPuzzle2;
    private bool isLocked;
    private bool isLockedSoundEffect;

    [SerializeField] GameObject player;
    private bool playerHasOrangeKey = false;
    private bool playerHasBlueKey = false;

    [SerializeField] AudioSource doorSoundEffect;
    [SerializeField] AudioSource doorKeySoundEffect;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        openingAlarmParent = GameObject.Find("AlarmParent");
        frontDoorTrigger = frontDoorController.GetComponent<TriggerDoor>().isTrigger;
        backDoorTrigger = backDoorController.GetComponent<TriggerDoor>().isTrigger;
        
    }

    // Update is called once per frame
    void Update()
    {
        frontDoorTrigger = frontDoorController.GetComponent<TriggerDoor>().isTrigger;
        backDoorTrigger = backDoorController.GetComponent<TriggerDoor>().isTrigger;
        playerHasOrangeKey = player.GetComponent<CharacterMovement>().hasOrangeKey;
        playerHasBlueKey = player.GetComponent<CharacterMovement>().hasBlueKey;
        if (frontDoorTrigger||backDoorTrigger)
        {
            if (isLockedDoorPuzzle2|| isLockedDoorPuzzle1)
            {
                isLocked = true;
                if(isLocked)
                {
                    if (alarm == null)
                    {
                        alarm = Instantiate(lockedAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                }
                if (isLockedDoorPuzzle1&& playerHasOrangeKey)
                {
                    isLocked = false;
                    isLockedSoundEffect = true;
                    player.GetComponent<CharacterMovement>().destroyKey();
                    if (alarm != null)
                    {
                        Destroy(alarm);
                        alarm = Instantiate(unLockedAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    else
                    {
                        alarm = Instantiate(unLockedAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    count++;
                    if(count >80)
                    {
                        isLockedDoorPuzzle1 = false;
                    }
                }    
                else if(isLockedDoorPuzzle2 && playerHasBlueKey)
                {
                    isLocked = false;
                    isLockedSoundEffect = true;

                    player.GetComponent<CharacterMovement>().destroyKey();
                    if (alarm != null)
                    {
                        Destroy(alarm);
                        alarm = Instantiate(unLockedAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    else
                    {
                        alarm = Instantiate(unLockedAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    count++;
                    if (count > 80)
                    {
                        isLockedDoorPuzzle2 = false;
                    }
                }
            }
            else
            { 
                if (!isOpen)
                {
                    if (alarm != null)
                    {
                        Destroy(alarm);
                        alarm = Instantiate(openingAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    else
                    {
                        alarm = Instantiate(openingAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                }
                else
                {
                    if (alarm != null)
                    {
                        Destroy(alarm);
                        alarm = Instantiate(closingAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                    else
                    {
                        alarm = Instantiate(closingAlarm, openingAlarmParent.transform);
                        alarm.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (alarm != null)
            {
                Destroy(alarm);
            }
        }
        if (frontDoorTrigger && !isLocked)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen) //if the door is closed
                {
                    if(isLockedSoundEffect)
                    {
                        doorKeySoundEffect.Play();
                        StartCoroutine(OpenWithDelay());
                    }
                    else
                    {
                        doorSoundEffect.Play();
                        isOpen = true;

                    }
                    myDoor.SetTrigger("frontDoorOpen");
                }
                else // if the door is open
                {
                    myDoor.SetTrigger("closeDoor");
                    doorSoundEffect.Play();

                    isOpen = false;
                }
            }
        }
        else if (backDoorTrigger && !isLocked)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen) //if the door is closed
                {
                    myDoor.SetTrigger("backDoorOpen");
                    if (isLockedSoundEffect)
                    {
                        doorKeySoundEffect.Play();
                        StartCoroutine(OpenWithDelay());
                    }
                    else
                    {
                        doorSoundEffect.Play();
                        isOpen = true;
                    }
                }
                else // if the door is open
                {
                    myDoor.SetTrigger("closeDoor");
                    doorSoundEffect.Play();

                    isOpen = false;
                }
            }
        }
    }
    IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isOpen = true;

    }

}
