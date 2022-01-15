using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInalScenceLoader : MonoBehaviour
{
    [SerializeField] GameObject frontDoorController;
    private bool frontDoorTrigger;
    [SerializeField] private GameObject openingAlarm;
    [SerializeField] private GameObject openingAlarmParent;
    private GameObject alarm;


    // Start is called before the first frame update
    void Start()
    {
        openingAlarmParent = GameObject.Find("AlarmParent");
    }

    // Update is called once per frame
    void Update()
    {
        frontDoorTrigger = frontDoorController.GetComponent<TriggerDoor>().isTrigger;
        if(frontDoorTrigger)
        {
            alarm = Instantiate(openingAlarm, openingAlarmParent.transform);
            alarm.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("RealFinalScene");
            }
        }
    }
}
