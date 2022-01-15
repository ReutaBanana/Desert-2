using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputMananger))]
public abstract class DeviceTracker : MonoBehaviour
{
    protected InputMananger im;
    protected InputData data;
    protected bool newData;

    private void Awake()
    {
        im = GetComponent<InputMananger>();
        data = new InputData(im.axisCount, im.buttonCount);
    }
    public abstract void Refresh(); 

}
