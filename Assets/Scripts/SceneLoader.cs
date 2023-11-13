using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    
    public static int LevelToLoad;
    // Start is called before the first frame update
  public static void LoadScene(int scene)
    {

        Movement move = GameObject.FindWithTag("player").GetComponent<Movement>();
        
        move.StartCoroutine(move.DoNotDestroy(scene));
      
    }

   public static void FinishLoading(int scene)
    {
        //Debug.Log("LoadingScene");
       
        LevelToLoad = scene;
        
        //Debug.Log($"Scene Index - {LevelToLoad}");
        SceneManager.LoadScene(10);
        Time.timeScale = 1.0f;

#if (UNITY_WSA) || (UNITY_STANDALONE_WIN) || (UNITY_WEBGL)
    Cursor.lockState = CursorLockMode.Locked;
#endif
    }

}
