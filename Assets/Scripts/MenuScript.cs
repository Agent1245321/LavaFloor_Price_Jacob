using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
        }
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(1);
        
        panel.SetActive(false);
    }

    public void LoadScene1()
    {
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(2);
        panel.SetActive(false);

    }

    public void Exit()
    {
        panel.SetActive(false);
    }

    public IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(3);
        
    }
}
