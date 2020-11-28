using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedOptions : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public GameObject instructionsPanel;
    [SerializeField] public GameObject keyMappingPanel;
    [SerializeField] public bool isPaused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }        
        if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        if(!pauseMenuUI.activeSelf)
        {
            pausePanel.SetActive(true);
        }
        pauseMenuUI.SetActive(true);
    }
    
    void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        pausePanel.SetActive(false);
        keyMappingPanel.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void GoBackToGame()
    {
        isPaused = !isPaused;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("menuScene");
    }

    public void KeyMappingMenu()
    {
        pausePanel.SetActive(false);
        keyMappingPanel.SetActive(true);
    }

    public void KeyMappingToPausedMenu()
    {
        pausePanel.SetActive(true);
        keyMappingPanel.SetActive(false);
    }

    public void InstructionsMenu()
    {
        pausePanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    
    public void InstructionsToPausedMenu()
    {
        instructionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
