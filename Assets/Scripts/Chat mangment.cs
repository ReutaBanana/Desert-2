using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatmangment : MonoBehaviour
{
    private string[] heroText = new string[] { "Huh?... Lighthouse? in the desert ? that's so strange....", "The candle is burning, someone must have been here beforee" };
    public bool textInProgress = false;
    private int textPlace=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < heroText.Length; i++)
        {
            if (!textInProgress)
            {
                textInProgress = true;

                this.gameObject.GetComponent<TypeWritingEffect>().fullText = heroText[textPlace];
                textPlace++;
            }
        }
    }
}
