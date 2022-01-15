using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWritingEffect : MonoBehaviour
{
    public string fullText;
    private string currentText="";
    public float delay=0.1f;
    public float timeToWaitBeforeDelete;

    private bool isPlaying = false;
    private void Update()
    {
        if(this.gameObject.GetComponent<TextManangment>().textInProgress && !isPlaying)
        {
            StartCoroutine(ShowText());
        }
    }
    IEnumerator ShowText()
    {
        isPlaying = true;
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
            if(i==fullText.Length-1)
            {
                yield return new WaitForSeconds(timeToWaitBeforeDelete);
                currentText = "";
                this.GetComponent<TextMeshProUGUI>().text = currentText;
                this.gameObject.GetComponent<TextManangment>().textInProgress = false;
                isPlaying = false;
            }
        }
    }
}
