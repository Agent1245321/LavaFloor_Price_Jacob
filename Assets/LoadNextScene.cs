using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("StartOnScene 0");
       StartCoroutine(LoadTheScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LoadTheScene()
    {
        Debug.Log("Creating ASync Operator");
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneLoader.LevelToLoad);
        Debug.Log("Created Async Opereator");
        Debug.Log("Tracking Progress");
        while (!loading.isDone)
        {
            Debug.Log(Mathf.Clamp01(loading.progress / .9f) * 100f);
            yield return null;
        }
        
        
    }
    
}
