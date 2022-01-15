using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManangment : MonoBehaviour
{
    public GameObject[] textTriggers;
    public bool textInProgress = false;
    private string[] heroText = new string[] { "Huh?... Lighthouse? in the desert? that's so strange....", "The fireplace is lit, someone must have been here beforee" };

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < textTriggers.Length; i++)
        {
            if(textTriggers[i].GetComponent<TriggerDoor>().isTrigger&&!textInProgress)
            {
                textInProgress = true;
                this.gameObject.GetComponent<TypeWritingEffect>().fullText = heroText[i];
            }
        }
    }
}
