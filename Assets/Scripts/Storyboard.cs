using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class Storyboard : MonoBehaviour
{    public VideoPlayer videoPlayer;
    public GameObject loadScreen;
    private RawImage loadScreenImage;
    public Animator sceneAnimator;
  
    public float fadeSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        sceneAnimator = loadScreen.GetComponent<Animator>();
        
        loadScreenImage = loadScreen.GetComponent<RawImage>();
        loadScreenImage.color = new Color(0, 0, 0, 0);
    }
    void Start()
    {
        videoPlayer.loopPointReached += onVideoEnd;
    }


    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.0f);
        loadScreen.SetActive(false);
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GoToGame();
        }
    }
    public void onVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToGame()
    {
        
        loadScreen.SetActive(true);
        sceneAnimator.Play("FadeOut");
        StartCoroutine("ChangeAnimation");
    }
    public IEnumerator ChangeAnimation()
    {
        yield return new WaitForSecondsRealtime(fadeSpeed);
        SceneManager.LoadScene("MainMenu");
    }
}
