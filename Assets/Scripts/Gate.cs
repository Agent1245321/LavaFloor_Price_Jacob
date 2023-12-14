using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    Movement ballScript;
    SpawnScript startData;

    public bool useStartData;

    public int crystalcount;
    bool setActive = true;
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
        crystalsCollected = ballScript.crystals;
        
        if (useStartData)
        {

            if (startData.crystalsInLevel - crystalcount <= crystalsCollected && setActive)
            {
                Debug.Log("Open The Gate (used start Data)");
                setActive = false;
                this.gameObject.SetActive(setActive);
            }

        }
        else
        {
            if (crystalcount <= crystalsCollected && setActive)
            {
                Debug.Log("Open The Gate");
                setActive = false;
                this.gameObject.SetActive(setActive);
            }
        }
    }
}
