using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterctAble : MonoBehaviour
{
    [SerializeField] private GameObject interactAlarm;
    [SerializeField] private GameObject openingAlarmParent;

    private GameObject alarm;

    private void Start()
    {
        openingAlarmParent = GameObject.Find("AlarmParent");

    }
    // Update is called once per frame
    void Update()
    {
     if(this.gameObject.GetComponent<TriggerDoor>().isTrigger)
        {
            if (alarm == null)
            {
                alarm = Instantiate(interactAlarm, openingAlarmParent.transform);
                alarm.SetActive(true);
            }
        }
     else
        {
            Destroy(alarm);
        }
    }
}
