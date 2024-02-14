using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public MenuScript manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager").GetComponent<MenuScript>();
        Debug.Log("StartOnScene 0");
       StartCoroutine(LoadTheScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LoadTheScene()
    {
        
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneLoader.LevelToLoad);

        //FIX THIS PLEAS RAHHHHHHHHHHHHHHHH
            manager.levelData[SceneLoader.LevelToLoad - 1] = true;
        
        manager.SaveGame();
        manager.setButtonsTF();
        while (!loading.isDone)
        {
            Debug.Log(Mathf.Clamp01(loading.progress / .9f) * 100f);
            yield return null;
        }
        
        
    }
    
}
