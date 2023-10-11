using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Movement : MonoBehaviour
{
    private AudioSource lavaSound;
    public AudioSource deathSound;
    private Rigidbody ball;
    int xValue;
    int yValue;
    int zValue;
    Wiggly wiggly;
    bool isGrouded;
    public bool isOnWall;
    Vector3 wallTouchPoint;
    Vector3 wallOutVector;
    public int crystals;

    Vector3 lookingTransform;
    Vector3 lookingTransformNoY;
    public GameObject cam;
    private GameObject spawn;
    private SpawnScript spawnData;
    private bool collectedWin = false;
    private Vector2 move;
    public static Vector2 look;


    private float stopping;
    public GameObject panel;
    public LightLauncher cannon;
    // Start is called before the first frame update

    private void Awake()
    {
        ball = this.GetComponent<Rigidbody>();
        lavaSound = this.transform.root.Find("GameObject").GetComponentInChildren<AudioSource>();
        panel = GameObject.FindWithTag("panel");
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
        ball.velocity = new Vector3(0, 0, 0);
        isGrouded = false;
        crystals = 0;
        Debug.Log($"Scene Loaded: Mode - {mode}");
        spawnData = GameObject.FindWithTag("spawn").GetComponent<SpawnScript>();
        spawn = GameObject.FindWithTag("spawn");
        Debug.Log("Spawn_Info-Pulled" + $" - {spawn}");
        ball.transform.position = spawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnSlow(InputValue value)
    {
       stopping = value.Get<float>();
    }

    private void SlowPlayer()
    {
        if (isGrouded)
        {
            ball.velocity = new Vector3(ball.velocity.x / (1 + stopping / 10), ball.velocity.y, ball.velocity.z / (1 + stopping / 10));
            
        }
        
        
    }
    
    public void OnLook(InputValue value)
    {
        
        look = value.Get<Vector2>();
        
    }
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        
        
    }

    public void OnPause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        panel.SetActive(true);
        DumbHideScript.hide = !DumbHideScript.hide;
        
    }

    private void MovePlayer()
    {
        lookingTransform = (cam.transform.forward.normalized * move.y) + (cam.transform.right.normalized * move.x);
        lookingTransformNoY = new Vector3(lookingTransform.x, 0, lookingTransform.z);
        ball.AddForce(lookingTransformNoY * 1000 * Time.deltaTime);
    }

    public void OnJump()
    {
        Debug.Log("Jump!");
        if (isOnWall)
        {

            ball.AddForce(wallOutVector * 10 + new Vector3(0, 7, 0), ForceMode.VelocityChange);

        }
        else if (isGrouded)
        {
            Debug.Log("JUMPED");
            ball.AddForce(0, 7, 0, ForceMode.VelocityChange);
        }

        if(cannon != null) { cannon.Launch(); }
    }

    public void OnReset()
    {
        Death();
        
    }

    private void FixedUpdate()
    {
       
        lavaSound.volume = 10 / this.transform.position.y;
        MovePlayer();
        SlowPlayer();
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "crystal")
        {
            StartCoroutine(other.GetComponent<Crystal>().DestroySelf());
            Debug.Log("Collided");
            crystals++;
            if(other.name == "win")
            {
                collectedWin = true;
            }
            StartCoroutine(Ahaha());
        }

        if(other.tag == "lava")
        {
            Death();
        }

        
    }

   // private void OnCollisionEnter(Collision collided)
  //  {
        
        
   // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "lava")
        { Death(); }

        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Floor") isGrouded = true;
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Touching Wall");
            isOnWall = true;
            wallTouchPoint = collision.GetContact(0).point;
            wallOutVector = (ball.transform.position - wallTouchPoint).normalized;
            
        }
       /* if (stopping)
        {
            ball.velocity = new Vector3(ball.velocity.x / 1.1f, ball.velocity.y, ball.velocity.z / 1.1f);
        }
       */
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.tag == "Floor") isGrouded = false;
        
    }

    private IEnumerator Ahaha()
    {
        if(spawnData.useCrystals)
        {
            if (crystals >= spawnData.crystalsInLevel) 
            {
                ball.velocity = new Vector3(0, 0, 0);
;                Debug.Log("Level Complete");
                DontDestroyOnLoad(this.transform.parent.gameObject);
                SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StopAllCoroutines();
                yield return null;
            }
            
        }
        else
        {
            if(collectedWin)
            {
                DontDestroyOnLoad(this.transform.parent.gameObject);
                ball.velocity = new Vector3(0, 0, 0);
                Debug.Log("Level Complete");
                SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StopAllCoroutines();
                yield return null;
            }
        }
    }

     
    

    void Death()
    {
        ball.constraints = RigidbodyConstraints.FreezeAll;
        if (cannon != null) { cannon = null; this.transform.parent.parent = null; DontDestroyOnLoad(this.transform.parent); }
        Checkpoint.isDead = true;
       
        deathSound.Play();
        ball.velocity = new Vector3(0, 0, 0);
        ball.angularVelocity = new Vector3(0, 0, 0);
        ball.constraints = RigidbodyConstraints.None;
        ball.transform.position = spawn.transform.position;
        
    }


    public IEnumerator DoNotDestroy(int Scene)
    {
        Console.WriteLine("DO Destroy");
        DontDestroyOnLoad (this.transform.parent.gameObject);
        Console.WriteLine("DO NOT DESTROY");
        SceneLoader.FinishLoading(Scene);
        yield return null;
    }
    void LevelComplete()
    {

    }
}
