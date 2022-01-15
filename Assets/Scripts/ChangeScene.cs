using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name=="OpeningScene")
        {
            StartCoroutine(ChangeAfter2SecondsCoroutine());
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }
    IEnumerator ChangeAfter2SecondsCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GamePlay");
}
    public void FinalDecision()
    {
        SceneManager.LoadScene("Oasis");
    }    public void ExitGame()
    {
        Application.Quit();
    }
}
