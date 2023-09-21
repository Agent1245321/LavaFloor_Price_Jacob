using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{

    public GameObject panel;
    private int screen;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject options;

    
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*  if(Input.GetKey(KeyCode.Escape))
          {
              panel.SetActive(true);
          }
        */
    }

    public void Next()
    {
        if (screen < 1) screen++;
        UpdateScreen();

    }

    public void Back()
    {
        if (screen > 0) screen--;
        UpdateScreen();

    }
    public void Options()
    {
        if (screen != 2) screen = 2;
        else screen = 0;
        UpdateScreen();

    }

    private void UpdateScreen()
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        options.SetActive(false);
        Debug.Log($"Current Screen is {screen}");
        switch (screen)
        {

            case 0:
                screen1.SetActive(true);
                break;

            case 1:
                screen2.SetActive(true);
                break;

            
            case 2:
                options.SetActive(true);
                break;

            default:
                screen1.SetActive(true);
                break;
        }
    }
    public void LoadScene0()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(1);

        panel.SetActive(false);
    }
    public void UpdateVolume(float value)
    {
        
        masterMixer.SetFloat("Master", value - 80);
    }

    public void UpdateEffectsVolume(float value)
    {

        masterMixer.SetFloat("Effects", value - 80);
    }

    public void UpdateLavaVolume(float value)
    {

        masterMixer.SetFloat("Lava", value - 80);
    }

    public void LoadScene1()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(2);
        panel.SetActive(false);

    }

    public void LoadScene2()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(3);

        panel.SetActive(false);
    }

    public void LoadScene3()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(4);
        panel.SetActive(false);

    }

    public void LoadScene4()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(5);
        panel.SetActive(false);

    }

    public void LoadScene5()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(6);
        panel.SetActive(false);

    }

    public void LoadScene6()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(7);
        panel.SetActive(false);

    }

    public void LoadScene7()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(8);
        panel.SetActive(false);

    }

    public void LoadScene8()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(9);
        panel.SetActive(false);

    }
    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);

    }

    public IEnumerator LevelStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(3);

    }

    public void Clicked()
    {
        Debug.Log("Clicked");
    }

    public void Open()
    {
        panel.SetActive(true);
    }
}