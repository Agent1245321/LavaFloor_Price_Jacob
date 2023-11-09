using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{

    public GameObject panel;
    public GameObject mobileControls;
    private int screen;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject options;
    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider lavaVolumeSlider;
    public Slider sensitivitySlider;
    public Toggle invertYToggle;
    public Toggle invertXToggle;

    public CamScript cam;

    
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        UpdateVolume(masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 80));
        UpdateEffectsVolume(effectsVolumeSlider.value = PlayerPrefs.GetFloat("effectsVolume", 80));
        UpdateLavaVolume(lavaVolumeSlider.value = PlayerPrefs.GetFloat("lavaVolume", 80));
      cam.UpdateSensitivity(sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 80));
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

    public void UpdateToggles()
    {
        if(cam.yInv == 1) {invertYToggle.isOn = false;}
        else if(invertYToggle.isOn == false)
        { 
            
            invertYToggle.isOn = true;
            cam.ToggleY();
        }

        if (cam.xInv == 1) { invertXToggle.isOn = false; }
        else if (invertXToggle.isOn == false)
        { invertXToggle.isOn = true;
            cam.ToggleX();
        }

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
       // Debug.Log($"Current Screen is {screen}");
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
                UpdateToggles();
                break;

            default:
                screen1.SetActive(true);
                break;
        }
    }
    public void LoadScene0()
    {
        //Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(1);

        panel.SetActive(false);
        mobileControls.SetActive(true);

    }
    public void UpdateVolume(float value)
    {
        
        masterMixer.SetFloat("Master", value - 80);
        PlayerPrefs.SetFloat("masterVolume", value);

    }

    public void UpdateEffectsVolume(float value)
    {

        masterMixer.SetFloat("Effects", value - 80);
        PlayerPrefs.SetFloat("effectsVolume", value);
    }

    public void UpdateLavaVolume(float value)
    {

        masterMixer.SetFloat("Lava", value - 80);
        PlayerPrefs.SetFloat("lavaVolume", value);
    }

    public void LoadScene1()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(2);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene2()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(3);

        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene3()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(4);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene4()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(5);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene5()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(6);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene6()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(7);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene7()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(8);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene8()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(9);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }
    public void Exit()
    {
       // Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public IEnumerator LevelStart()
    {
       // Cursor.lockState = CursorLockMode.Locked;
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