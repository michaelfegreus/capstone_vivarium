using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FMVManager : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    [SerializeField] GameObject cam;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        PlayerManager.Instance.EnterMenuState();
        FindObjectOfType<QuestTrackerCompanion>().HideQuests();
        StartCoroutine("ShowCam");
    }

    IEnumerator ShowCam()
    {
        yield return new WaitForSeconds(0.3f);
        cam.SetActive(true);

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.Stop();
        PlayerManager.Instance.EnterFreeState();
        cam.SetActive(false);
        FindObjectOfType<QuestTrackerCompanion>().ShowQuests();
    }

    void Update()
    {
        
    }
}
