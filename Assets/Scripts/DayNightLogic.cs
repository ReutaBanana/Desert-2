using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLogic : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] float timeToSwitch;

    [SerializeField] GameObject trigger = null;
    [SerializeField] GameObject lightDay;
    [SerializeField] GameObject lightNight;

    float rapidScrollAmount = 0.05f;
    float scrollAmount = 0.01f;
    private float timer;
    float originOfffset;
    float offset;

    public bool isMainMenu;
    void Start()
    {
        timer = timeToSwitch;
        sky.mainTextureOffset = new Vector2(4.22f, 0);
        originOfffset = 4.22f;
        this.gameObject.GetComponent<MeshRenderer>().material = sky;

    }

    void Update()
    {
        TimerSunset(1);
        if (trigger.GetComponent<SunsetTrigger>().isTrigger||isMainMenu)
        {
            TimerSunset(2);
        }
    }

    public void TimerSunset(int sunsetZone)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if(sunsetZone == 1)
            {
                Sunset();
                timer = timeToSwitch;
            }
            else if (sunsetZone == 2)
            {
                RapidSunset();
                timer = timeToSwitch;
            }
           
        }
    }
    public void Sunset()
    {
        if (offset < 4.3)
        {
            offset = (originOfffset + scrollAmount);
            originOfffset = offset;
            sky.mainTextureOffset = new Vector2(offset, 0);
            lightDay.SetActive(true);
            lightNight.SetActive(false);
        }
    }
    public void RapidSunset()
    {
        if (offset < 4.4)
        {
            offset = (originOfffset + rapidScrollAmount);
            originOfffset = offset;
            sky.mainTextureOffset = new Vector2(offset, 0);
            lightDay.SetActive(true);
            lightNight.SetActive(false);
        }
        else
        {
            lightNight.SetActive(true);
            lightDay.SetActive(false);
        }
    }
}
