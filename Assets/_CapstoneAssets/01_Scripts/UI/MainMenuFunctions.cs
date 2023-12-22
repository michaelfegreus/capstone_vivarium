using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField]private Animator fadeAnim;
    [SerializeField]private string sceneName;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            StartCoroutine("WaitForPopIn");
        }
    }

    public void StartGame()
    {
        fadeAnim.SetTrigger("FadeToWhite");
        StartCoroutine("WaitForFade");
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(3f);
        LoadGameScene();
    }

    IEnumerator WaitForPopIn()
    {
        yield return new WaitForSeconds(0.1f);
        fadeAnim.SetTrigger("FadeAway");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

}
