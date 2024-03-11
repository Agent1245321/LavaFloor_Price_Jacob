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
using UnityEditor;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    public LeaderBoard leaderBoard;

    public GameObject panel;
    public GameObject panel2;
    public GameObject mobileControls;
    private int screen;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject Leaderboard1;
    public GameObject Leaderboard2;
    public GameObject Leaderboard3;
    public GameObject Leaderboard4;


    public GameObject shop1;
    public GameObject shop2;
    public GameObject shop3;
    public GameObject shop4;

    public GameObject options;
    public GameObject bNButtons;
    public Slider masterVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider lavaVolumeSlider;
    public Slider sensitivitySlider;
    public Toggle invertYToggle;
    public Toggle invertXToggle;

    public PlayerInput input;
    public static int scheme;

    public CamScript cam;


    //fun data stuff
    public  bool[] levelData;

    public bool[] skinData;

    public int crystalsData;

    [SerializeField]
    public Button[] buttons;

    [SerializeField]
    public float[] timers;






    public LeaderBoard leaderBoardi;

    public ShopScript shopScript;
    

    
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

        if (cam.xInv == 1) 
        { invertXToggle.isOn = false; }
        else if (invertXToggle.isOn == false)
        { invertXToggle.isOn = true;
            cam.ToggleX();
        }

    }

    public int vCamOn;
    public void UseVirtualCamera()
    {
        if (cam.yInv == 1)
        { invertYToggle.isOn = false; }
        else if (invertYToggle.isOn == false)
        {

            invertYToggle.isOn = true;
            cam.ToggleY();
        }

        if (cam.xInv == 1) { invertXToggle.isOn = false; }
        else if (invertXToggle.isOn == false)
        {
            invertXToggle.isOn = true;
            cam.ToggleX();
        }
    }

    public void Next()
    {
        if (screen < 1 || (screen >=5 && screen < 8) || (screen >= 11 && screen < 14)) screen++;
        UpdateScreen();
        Debug.Log(screen);

    }

    public void Back()
    {
        if (screen > 0 && 4 > screen || (screen > 5 && screen <= 8) || (screen > 11 && screen <= 14)) screen--;
        UpdateScreen();
        Debug.Log(screen);

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

    public void Shop()
    {
        shop1.SetActive(true);
        shop2.SetActive(true);
        shop3.SetActive(true);
        shop4.SetActive(true);
        shopScript.GetToggles();
        bNButtons.gameObject.SetActive(true);
        if (screen != 11)
        {
            screen = 11;
           


        }

        else
        {
            screen = 0;

        }

        UpdateScreen();
        Debug.Log(screen);




    }

    private void UpdateScreen()
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        Leaderboard1.SetActive(false);
        Leaderboard2.SetActive(false);
        Leaderboard3.SetActive(false);
        Leaderboard4.SetActive(false);
        shop1.SetActive(false);
        shop2.SetActive(false);
        shop3.SetActive(false);
        shop4.SetActive(false);
        

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
                UpdateLeaderboards(0);
                UpdateLeaderboards(1);
                UpdateLeaderboards(2);
                break;

            case 6:
                Leaderboard2.SetActive(true);
                UpdateToggles();
                UpdateLeaderboards(3);
                UpdateLeaderboards(4);
                UpdateLeaderboards(5);
                break;

            case 7:
                Leaderboard3.SetActive(true);
                UpdateToggles();
                UpdateLeaderboards(6);
                UpdateLeaderboards(7);
                UpdateLeaderboards(8);
                
                break;
            case 8:
                Leaderboard4.SetActive(true);
                UpdateToggles();
                UpdateLeaderboards(9);
                UpdateLeaderboards(10);
                UpdateLeaderboards(11);

                break;

            case 11:
                shop1.SetActive(true);
                break;

            case 12:
                shop2.SetActive(true);
                break;
            
            case 13:
                shop3.SetActive(true);

                break;

            case 14:
                shop4.SetActive(true);

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
        string schem = input.currentControlScheme;
        switch (schem)
        {
            case "Touch":
                scheme = 0;
                break;

            case "Keyboard&Mouse":
                scheme = 1;
                break;

            case "Gamepad":
                scheme= 2;
                break;

            default:
                scheme = 2;
                break;
        }

        SaveGame();
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneLoader.LoadScene(indxOf);
        panel.SetActive(false);
        panel2.SetActive(true);
        
        
        StartTimer(indxOf  - 1);
#if (UNITY_ANDROID || UNITY_IOS)
        mobileControls.SetActive(true);
#endif


    }

   
    public void Exit()
    {
        shopScript.UpdateUI();
        if (!panel.gameObject.activeSelf || screen != 0)
        { 
        #if (UNITY_IOS || UNITY_ANDROID)
          mobileControls.SetActive(false);
        #endif
       
        #if (UNITY_WSA || UNITY_STANDALONE || UNITY_WEBGL)
            Cursor.lockState = CursorLockMode.Confined;
        #endif

            setButtonsTF();
            input.SwitchCurrentActionMap("UI");
            panel.SetActive(true);
            panel2.SetActive(false);
            Time.timeScale = 0;
            DumbHideScript.hide = true;
            screen = 0;
            bNButtons.gameObject.SetActive(true);
            }

        //Resumes The Game
        else if (panel.gameObject.activeSelf && screen == 0)
        {
            ResumeGame();
        }

        UpdateScreen();
    }

    public void ResumeGame()
    {
        input.SwitchCurrentActionMap("Player");
        panel.SetActive(false);
        panel2.SetActive(true);
        Time.timeScale = 1;
        DumbHideScript.hide = false;
        Time.timeScale = 1;

        //Configure According to device

            #if (UNITY_ANDROID || UNITY_IOS)
                 mobileControls.SetActive(true);
            #endif

            #if (UNITY_WSA || UNITY_STANDALONE || UNITY_WEBGL)
                 Cursor.lockState = CursorLockMode.Locked;
            #endif
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

    //load game method
    public void loadGame()
    {
        //gathers the returned data types from the loadGame method;
        bool[] oldLevelData = PlayerData1.LoadGame().unlockedLevels;
        float[] oldtimers = PlayerData1.LoadGame().timers;
       bool[] oldSkinData = PlayerData1.LoadGame().unlockedSkins;

        oldLevelData.CopyTo(levelData, 0);
        oldSkinData.CopyTo(skinData, 0);
        oldtimers.CopyTo(timers, 0);

        crystalsData = PlayerData1.LoadGame().crystals;



        panel.SetActive(true);
        shop1.SetActive(true);
        shop2.SetActive(true);
        shop3.SetActive(true);
        shop4.SetActive(true);
        shopScript.GetToggles();
       // Debug.Log("Length of skinData" + skinData.Length);
        foreach (bool inyer in skinData)
        {
           // Debug.Log("skindata : " + inyer);
        }
        
        //Debug.Log("Crystals : " + crystalsData);
        shopScript.loadGame(skinData, crystalsData);

        shop1.SetActive(false);
        shop2.SetActive(false);
        shop3.SetActive(false);
        shop4.SetActive(false);

      //  Debug.Log("Loaded Player Save Data at:" + Application.persistentDataPath + "/player.fun");
        setButtonsTF();
        timerText = "Records:\n";
        int i = 1;
        foreach (float time in timers)
        {
            timerText += $"Level {i}: {time}\n";
            i++;
        }
     //   Debug.Log(timerText);
    }

    public void SaveGame() 
    {
        foreach (var level in levelData) { Debug.Log(level); }
        shopScript.save();
        PlayerData1.SaveGame(this);
       // Debug.Log("Saved Game");
        
    }


    public TextMeshProUGUI recordsOutput;
    public void setButtonsTF()
    {
       // Debug.Log("Setting Buttons");
        
        int level = 0;
        foreach (Button button in buttons)
        {
            
            button.interactable = levelData[level];
            level++;
        };

        recordsOutput.text = timerText;

        
    }

    public void code(string code)
    {
        if (code == "atpunpacbcefmnl")
        {


            //Debug.Log("Unlocking Levels");
            int level = 0;
            foreach (Button button in buttons)
            {

                button.interactable = true;
                level++;
            };
        }
    }

    public GameObject[] leaderboards;
    void UpdateLeaderboards(int indx)
    {
        
        
            leaderboards[indx].transform.Find("Names").GetComponent<TextMeshProUGUI>().text = leaderBoardi.leaderboardsNames[indx];
            leaderboards[indx].transform.Find("Scores").GetComponent<TextMeshProUGUI>().text = leaderBoardi.leaderboardsScores[indx];
        
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
     "Level8Time",
     "Level9Time",
     "Level10Time",
     "Level11Time",
     "Level12Time"
    };
    public void StopTimer()
    {
        timerGo = false;

        if (timers[levelIndxTimer] == 0)
        {
            timers[levelIndxTimer] = timeInSeconds;
            Debug.Log("Set the first Record:" + timeInSeconds);
            StartCoroutine(leaderBoard.SubmitScoreRoutine(Mathf.RoundToInt(timeInSeconds * 1000), keys[levelIndxTimer]));
        }
        else if (timers[levelIndxTimer] > timeInSeconds)
        {
            timers[levelIndxTimer] = timeInSeconds;
            Debug.Log("Set a new Record:" + timeInSeconds + "... \n");
            StartCoroutine(leaderBoard.SubmitScoreRoutine(Mathf.RoundToInt(timeInSeconds * 1000), keys[levelIndxTimer]));
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
        //Debug.Log(timerText);
        
    }

    void Update()
    {
        if (timerGo)
        {
            timeInSeconds += Time.deltaTime;
           // Debug.Log(timeInSeconds);
        }
    }

    public TextMeshProUGUI uiTimer;
    private void LateUpdate()
    {
        uiTimer.text = timeInSeconds.ToString();
    }
}