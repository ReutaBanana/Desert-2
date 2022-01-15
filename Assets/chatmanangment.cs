using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatmanangment : MonoBehaviour
{
    private string[] heroText = new string[] { "Huh?... Lighthouse? in the desert ? that's so strange....", "The candle is burning, someone must have been here beforee" };
    public bool textInProgress = false;
    private int textPlace = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<TypeWritingEffect>().fullText = heroText[0];

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&!textInProgress)
        {
            textPlace++;
            textInProgress = true;
            this.gameObject.GetComponent<TypeWritingEffect>().fullText = heroText[textPlace];

        }
    }
}