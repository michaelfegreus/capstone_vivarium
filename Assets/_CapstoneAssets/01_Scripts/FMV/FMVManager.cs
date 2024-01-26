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
        cam.SetActive(true);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.Stop();
        PlayerManager.Instance.EnterFreeState();
        cam.SetActive(false);
    }

    void Update()
    {
        
    }
}
