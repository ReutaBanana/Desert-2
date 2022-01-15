using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject inGameMenu;
    private bool isInGameMenuActive = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&& !isInGameMenuActive)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isInGameMenuActive)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        inGameMenu.SetActive(false);
        isInGameMenuActive = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isInGameMenuActive = true;
        inGameMenu.SetActive(true);
    }
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
