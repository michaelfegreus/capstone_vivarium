using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FMVHandler : MonoBehaviour
{

    [SerializeField] private string sceneName;
    [SerializeField] private Animator fadeAnim;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            Debug.Log("Waiting for Pop In");

            StartCoroutine("WaitForPopIn");
        }
    }

    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        fadeAnim.Play("WhiteStay");
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator WaitForPopIn()
    {
        yield return new WaitForSeconds(3f);
        fadeAnim.SetTrigger("FadeAway");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
