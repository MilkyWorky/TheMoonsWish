using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider sliderLoad;

    public AudioSource bgm;
    public AudioClip bgmClip;
    public AudioSource audioButton;
    public AudioClip hoverButton;
    public AudioClip pressedButton;

    public GameObject Title;

    public GameObject tutorial;

    private void Start()
    {
        bgm.clip = bgmClip;
        bgm.Play();
    }


    public void StartGame()
    {
        Title.SetActive(false);
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            loadingScreen.SetActive(true);

            //float progessValue = Mathf.Clamp01(operation.progress / 0.9f);
            float progessValue = operation.progress;

            sliderLoad.value = progessValue;

            yield return null;
        }
    }


    public void AudioHover()
    {
        audioButton.clip = hoverButton;
        audioButton.Play();
    }

    public void AudioPressed()
    {
        audioButton.clip = pressedButton;
        audioButton.Play();
    }

    public void ExotGamu()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        tutorial.SetActive(true);
    }

    public void TutorialBack()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            tutorial.SetActive(false);
        }
        tutorial.SetActive(false);
    }

}
