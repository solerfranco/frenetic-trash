using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

[RequireComponent(typeof(VideoPlayer))]
public class VidPlayer : MonoBehaviour
{
    [SerializeField]
    private string _videoFileName;

    public int levelIndex = 2;
    void Start()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, _videoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
    }

    public void EndReached(VideoPlayer source)
    {
        LoadNewLevel();
    }

    public void LoadNewLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
