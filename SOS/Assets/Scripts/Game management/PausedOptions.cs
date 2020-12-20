using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedOptions : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public GameObject instructionsPanel;
    [SerializeField] public GameObject keyMappingPanel;
    [SerializeField] public bool isPaused;

    [SerializeField] public GameObject mainPlayer;

    [SerializeField] private bool hasDeactivated;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }        
        if(isPaused)
        {
            hasDeactivated = false;
            ActivateMenu();
        }
        else
        {
            if(!hasDeactivated)
            {
                DeactivateMenu();
            }
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        ThirdPersonMovement options = mainPlayer.GetComponent<ThirdPersonMovement>();
        options.gameJustResumed = true;
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
        ThirdPersonMovement options = mainPlayer.GetComponent<ThirdPersonMovement>();
        options.gameJustResumed = false;
        hasDeactivated = true;
    }

    public void GoBackToGame()
    {
        isPaused = !isPaused;
    }

    public void GoToMainMenu()
    {
        try
        {
            AudioManager manager = FindObjectOfType<AudioManager>();
            manager.Stop("MainMusic");
            manager.Stop("BananaMusic");
            manager.Stop("OldLadyAbilityMusic");
            manager.Stop("HippieAbilityMusic");
            manager.Stop("RadarBlip");
            manager.Play("Jungle");
        }
        catch(Exception)
        {
            Debug.Log("Error with audio");
        }
        finally
        {
            SceneManager.LoadScene("menuScene");
        }
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

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exit");
    }

    public void ToggleMainMusic()
    {
        AudioManager manager = FindObjectOfType<AudioManager>();
        bool currentValue = manager.shouldPlayMainMusic;
        if(currentValue)
            manager.StopMainMusic();
        else
        {
            ThirdPersonMovement player = FindObjectOfType<ThirdPersonMovement>();
            manager.ResumeMainMusic();
            if(!player.isUsingBanana && !player.abilityActive)
                manager.PlayMainMusic();
        }
    }
}
