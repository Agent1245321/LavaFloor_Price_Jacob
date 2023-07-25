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
      /*  if(Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
        }
      */
    }

    public void LoadScene0()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(1);
        
        panel.SetActive(false);
    }

    public void LoadScene1()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(2);
        panel.SetActive(false);

    }

    public void LoadScene2()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(3);

        panel.SetActive(false);
    }

    public void LoadScene3()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(4);
        panel.SetActive(false);

    }

    public void LoadScene4()
    {
        Debug.Log("LoadingScene");
        StartCoroutine(LevelStart());
        SceneManager.LoadScene(5);
        panel.SetActive(false);

    }
    public void Exit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
        
    }

    public IEnumerator LevelStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(3);
        
    }

    public void Clicked()
    {
        Debug.Log("Clicked");
    }

    public  void Open()
    {
        panel.SetActive(true);
    }
}
