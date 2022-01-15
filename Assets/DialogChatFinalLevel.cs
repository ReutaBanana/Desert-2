using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogChatFinalLevel : MonoBehaviour
{
    public string fullText="Welcome to my lightHousee";
    private string currentText = "";
    public float delay = 0.1f;
    public float timeToWaitBeforeDelete;
    private bool isPlaying;
    private int textplace=1;
    [SerializeField] GameObject alarm;
    [SerializeField] GameObject chooseWisely;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());

    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying&Input.GetKeyDown(KeyCode.E)&&textplace==1)
        {
            alarm.SetActive(false);

            fullText = "I am the lighthouse watcher, guardian of lost souls....";
            StartCoroutine(ShowText());
            textplace++;
        }
        if (!isPlaying & Input.GetKeyDown(KeyCode.E) && textplace ==2)
        {
            alarm.SetActive(false);

            fullText = "It has been many years since I had a visitor....";
            StartCoroutine(ShowText());
            textplace++;
        }
        if (!isPlaying & Input.GetKeyDown(KeyCode.E) && textplace == 3)
        {
            alarm.SetActive(false);

            fullText = "you have proved worthy. I will grant you one wish..";
            StartCoroutine(ShowText());
            textplace++;

        }
        if (!isPlaying & Input.GetKeyDown(KeyCode.E) && textplace == 4)
        {
            alarm.SetActive(false);
            fullText = "choose wisely..";
            StartCoroutine(ShowText());
            textplace++;

        }  
        if (!isPlaying & Input.GetKeyDown(KeyCode.E) && textplace == 5)
        {
            alarm.SetActive(false);

            chooseWisely.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        }

    }
    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            isPlaying = true;
            yield return new WaitForSeconds(delay);
            if (i == fullText.Length - 1)
            {
                yield return new WaitForSeconds(0.5f);
                alarm.SetActive(true);
                isPlaying = false;

            }
        }
    }
}
