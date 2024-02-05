using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate1 : MonoBehaviour
{
    Movement ballScript;
    SpawnScript startData;

    public bool offBeat;
    public GameObject bars;

  
   
    private int crystalsCollected;
    // Start is called before the first frame update
    void Start()
    {

        ballScript = GameObject.FindGameObjectWithTag("player").GetComponent<Movement>();
        startData = GameObject.FindGameObjectWithTag("spawn").GetComponent<SpawnScript>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Started?");
        ballScript = GameObject.FindGameObjectWithTag("player").GetComponent<Movement>();
        startData = GameObject.FindGameObjectWithTag("spawn").GetComponent<SpawnScript>();
        Debug.Log(ballScript.name);
        Debug.Log(startData);
    }

        // Update is called once per frame
        void Update()
        {
        if(offBeat) { crystalsCollected = ballScript.crystals + 1; }
        else
        {
            crystalsCollected = ballScript.crystals;
        }
        
        
        
        

            if (crystalsCollected % 2 == 0)
            {
            Debug.Log("Open The Gate");

            bars.gameObject.SetActive(true);
        }
            else
        {
            Debug.Log("Open The Gate");

            bars.gameObject.SetActive(false);
        }

        
       
    }
}
