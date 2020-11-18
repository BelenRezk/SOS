using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void SelectCharacter()
    {
        SceneManager.LoadScene("characterSelectionScene");
    }

    public void GameInstructions()
    {
        SceneManager.LoadScene("instructionsScene");
    }

    public void GameKeyMapping()
    {
        SceneManager.LoadScene("keyMappingScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}
