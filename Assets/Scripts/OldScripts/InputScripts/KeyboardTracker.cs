using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTracker : DeviceTracker
{
    public AxisKeys[] axisKeys;
    public KeyCode[] buttonKeys;

    private void Reset()
    {
        im = GetComponent<InputMananger>();
        axisKeys = new AxisKeys[im.axisCount];
        buttonKeys = new KeyCode[im.buttonCount];
    }

    void Update()
    {
        for (int i = 0; i < buttonKeys.Length; i++)
        {
            if(Input.GetKey(buttonKeys[i]))
            {
                data.buttons[i] = true;
                newData = true;
            }
        }
        for (int i = 0; i < axisKeys.Length; i++)
        {
            float val = 0f;
            if (Input.GetKey(axisKeys[i].positive))
            {
                val += 1f;
                //data.buttons[i] = true;
                newData = true;
            }
            if (Input.GetKey(axisKeys[i].negetive))
            {
                val -= 1f;
                //data.buttons[i] = true;
                newData = true;
            }
            if(Input.GetKey(buttonKeys[i]))
            {
                data.buttons[i] = true;
            }
            data.axes[i] = val;
        }
        if (newData)
        {
            im.PassInput(data);
            newData = false;
            data.Reset();
        }
    }
    public override void Refresh()
    {
        im = GetComponent<InputMananger>();

        KeyCode[] newButtons = new KeyCode[im.buttonCount];
        AxisKeys[] newAxes = new AxisKeys[im.axisCount];

        if(buttonKeys!=null)
        {
            for (int i = 0; i < Mathf.Min(newButtons.Length, buttonKeys.Length);i++)
            {
                newButtons[i] = buttonKeys[i];          
            }
            buttonKeys = newButtons;
        }

        if (axisKeys != null)
        {
            for (int i = 0; i < Mathf.Min(newAxes.Length, axisKeys.Length); i++)
            {
                newAxes[i] = axisKeys[i];
            }
            axisKeys = newAxes;
        }
    }
}
[System.Serializable]
public struct AxisKeys
{
    public KeyCode positive;
    public KeyCode negetive;
}
