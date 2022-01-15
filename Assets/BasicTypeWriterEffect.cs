using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicTypeWriterEffect : MonoBehaviour
{
    public string fullText;
    private string currentText = "";
    public float delay = 0.1f;
    public float timeToWaitBeforeDelete;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
            if (i == fullText.Length - 1)
            {
                yield return new WaitForSeconds(timeToWaitBeforeDelete);
                currentText = "";
                this.GetComponent<TextMeshProUGUI>().text = currentText;
            }
        }
    }
}
