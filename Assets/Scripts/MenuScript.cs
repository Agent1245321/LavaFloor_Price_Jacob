using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem.Composites;
using System.IO;
using TMPro;
using System;

public class MenuScript : MonoBehaviour
{
    public LeaderBoard leaderBoard;

    public GameObject panel;
    public GameObject mobileControls;
    private int screen;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject Leaderboard1;
    public GameObject Leaderboard2;
    public GameObject Leaderboard3;
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

    [SerializeField]
    public float[] timers;





    

    
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
        if (screen < 1 || (screen >=5 && screen < 7)) screen++;
        UpdateScreen();

    }

    public void Back()
    {
        if (screen > 0 && 4 > screen || (screen > 5 && screen <= 7)) screen--;
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

    public void Leaderboards()
    {
        bNButtons.gameObject.SetActive(true);
        if (screen != 5)
        {
            screen = 5;
            
        }

        else
        {
            screen = 0;
            
        }

        UpdateScreen();




    }

    private void UpdateScreen()
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        Leaderboard1.SetActive(false);
        Leaderboard2.SetActive(false);
        Leaderboard3.SetActive(false);
        

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

            case 5:
                Leaderboard1.SetActive(true);
                UpdateToggles();
                break;

            case 6:
                Leaderboard2.SetActive(true);
                UpdateToggles();
                break;

            case 7:
                Leaderboard3.SetActive(true);
                UpdateToggles();
                break;

            default:
                screen1.SetActive(true);
                break;
        }
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

    
    public void LoadScene(int indxOf)
    {

        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(indxOf);
        panel.SetActive(false);
        
        StartTimer(indxOf  - 1);

#if (UNITY_ANDROID || UNITY_IOS)
        mobileControls.SetActive(true);
#endif


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
        timers = PlayerData1.LoadGame().timers;
        Debug.Log("Loaded Player Save Data at:" + Application.persistentDataPath + "/player.fun");
        setButtonsTF();
        timerText = "Records:\n";
        int i = 1;
        foreach (float time in timers)
        {
            timerText += $"Level {i}: {time}\n";
            i++;
        }
        Debug.Log(timerText);
    }

    public void SaveGame() 
    {
        foreach (var level in levelData) { Debug.Log(level); }
        
        PlayerData1.SaveGame(this);
        Debug.Log("Saved Game");
        
    }


    public TextMeshProUGUI recordsOutput;
    public void setButtonsTF()
    {
        Debug.Log("Setting Buttons");
        
        int level = 0;
        foreach (Button button in buttons)
        {
            
            button.interactable = levelData[level];
            level++;
        };

        recordsOutput.text = timerText;

        
    }




    public static bool timerGo;
    public float timeInSeconds = 0;
    public int levelIndxTimer;
    public void StartTimer(int levelIndx) 
    {
        levelIndxTimer = levelIndx;
        timeInSeconds = 0;
        timerGo = true;
        Debug.Log("timer started");
        Debug.Log("Previus Record: " + timers[levelIndxTimer]);
    }

    public string timerText = "Records:\n";

    //These are the keys for the leaderboards//
    string[] keys = { 
     "Level1Time",
     "Level2Time",
     "Level3Time",
     "Level4Time",
     "Level5Time",
     "Level6Time",
     "Level7Time",
     "Level8Time" 
    };
    public void StopTimer()
    {
        timerGo = false;

        if (timers[levelIndxTimer] == 0)
        {
            timers[levelIndxTimer] = timeInSeconds;
            Debug.Log("Set the first Record:" + timeInSeconds);
            StartCoroutine(leaderBoard.SubmitScoreRoutine(Mathf.RoundToInt(timeInSeconds), keys[levelIndxTimer]));
        }
        else if (timers[levelIndxTimer] > timeInSeconds)
        {
            timers[levelIndxTimer] = timeInSeconds;
            Debug.Log("Set a new Record:" + timeInSeconds + "... \n");
            StartCoroutine(leaderBoard.SubmitScoreRoutine(Mathf.RoundToInt(timeInSeconds), keys[levelIndxTimer]));
        }
        else
        {
            Debug.Log("Your time did not beat the record");
        }

        int i = 1;
        timerText = "Records:\n";
        foreach (float time in timers) { 
            
            timerText += $"Level {i}: {time}\n";
            i++; }
        Debug.Log(timerText);

    }

    void Update()
    {
        if (timerGo)
        {
            timeInSeconds += Time.deltaTime;
           // Debug.Log(timeInSeconds);
        }
    }
}