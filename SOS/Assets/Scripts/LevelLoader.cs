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

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            if (progress < 1)
            {
                slider.value = progress;
                progressText.text = "" + (progress * 100f) + "%";
            }
            else
            {
                slider.value = 0.99f;
                progressText.text = "99% - Finalizing activation";
            }
            yield return null;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}