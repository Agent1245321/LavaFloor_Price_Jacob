using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem.Composites;
using System.IO;

public class MenuScript : MonoBehaviour
{

    public GameObject panel;
    public GameObject mobileControls;
    private int screen;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject options;
    public GameObject bNButtons;
    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider lavaVolumeSlider;
    public Slider sensitivitySlider;
    public Toggle invertYToggle;
    public Toggle invertXToggle;

    public CamScript cam;

    public  bool[] levelData;
    [SerializeField]
    public Button[] buttons;

    
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
       if(File.Exists(Application.persistentDataPath + "/player.fun") != true)
        {
            SaveGame();
            Debug.Log("Created New Player Save Data at:" + Application.persistentDataPath + "/player.fun");
        }
        else
        {
            loadGame();
            
        }
        
        setButtonsTF();
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
        if (screen != 2) 
        { 
            screen = 2;
            bNButtons.gameObject.SetActive(false); 
        }


        else 
        {
            screen = 0;
            bNButtons.gameObject.SetActive(true); 
        }

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
        
        SaveGame();
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
        
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(2);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene2()
    {
        
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(3);

        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene3()
    {
       
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(4);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene4()
    {
       
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(5);
        panel.SetActive(false);
        mobileControls.SetActive(true);

    }

    public void LoadScene5()
    {
        
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(6);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene6()
    {
       
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(7);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene7()
    {
        
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(8);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }

    public void LoadScene8()
    {
        
        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(9);
        panel.SetActive(false);
        mobileControls.SetActive(true);
    }
    public void Exit()
    {
        // Cursor.lockState = CursorLockMode.Locked;

        if (screen == 2)
        {
            screen = 0;
            bNButtons.gameObject.SetActive(true);
#if (UNITY_WSA || UNITY_STANDALONE || UNITY_WEBGL)
            Cursor.lockState = CursorLockMode.Confined;
#endif
        }
        else
        {
            panel.SetActive(false); Time.timeScale = 1;
#if (UNITY_ANDROID || UNITY_IOS)
        mobileControls.SetActive(true);
#endif
#if (UNITY_WSA || UNITY_STANDALONE || UNITY_WEBGL)
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }
        UpdateScreen();
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
#if (UNITY_WSA) || (UNITY_STANDALONE_WIN) || (UNITY_WEBGL)
        Cursor.lockState = CursorLockMode.Confined;
#endif
        panel.SetActive(true);
        Time.timeScale = 0;
    }


    public void loadGame()
    {

        levelData = PlayerData1.LoadGame().unlockedLevels;
        Debug.Log("Loaded Player Save Data at:" + Application.persistentDataPath + "/player.fun");
        setButtonsTF();
    }

    public void SaveGame() 
    {
        foreach (var level in levelData) { Debug.Log(level); }
        
        PlayerData1.SaveGame(this);
        Debug.Log("Saved Game");
    }

    public void setButtonsTF()
    {
        Debug.Log("Setting Buttons");
        Debug.Log(levelData.Length);
        Debug.Log(buttons.Length);
        int level = 0;
        foreach (Button button in buttons)
        {
            
            button.interactable = levelData[level];
            level++;
        };

        
    }
}