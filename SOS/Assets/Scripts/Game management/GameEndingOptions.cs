using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndingOptions : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("characterSelectionScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}
