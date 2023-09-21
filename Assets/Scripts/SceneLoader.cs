using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
   
    public static int LevelToLoad;
    // Start is called before the first frame update
  public static void LoadScene(int scene)
    {
        Debug.Log("LoadingScene");
        LevelToLoad = scene;
        Debug.Log($"Scene Index - {LevelToLoad}");
        SceneManager.LoadScene(10); 
    }
}
