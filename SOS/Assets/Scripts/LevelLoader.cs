using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        GameObject menu = GameObject.Find("Menu UI");
        menu.SetActive(false);       

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}