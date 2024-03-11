using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using LootLocker.Requests;

public class Movement : MonoBehaviour
{
   

    public AudioManager audioMngr;
    
    private Rigidbody ball;

    bool isGrouded;
    public bool isOnWall;
    Vector3 wallTouchPoint;
    Vector3 wallOutVector;
    public int crystals;

    Vector3 lookingTransform;
   
    public GameObject cam;
    private GameObject spawn;
    private SpawnScript spawnData;
    private bool collectedWin = false;
    private Vector2 move;
    public static Vector2 look;


    private float stopping;
    public GameObject panel;
    public GameObject panel2;
    public GameObject MobileControls;
    public LightLauncher cannon;

    

    private bool doubleJump;

    public int status = 1;

    public GameObject forwardObject;

    public ShopScript shop;
   

    private void Awake()
    {
        ball = this.GetComponent<Rigidbody>();
        
        audioMngr = this.GetComponent<AudioManager>();
       
        
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

#if UNITY_WSA
        LightmapSettings.lightmaps = new LightmapData[0];
        Resources.UnloadUnusedAssets();

#endif

        collectedWin = false;
        ball.velocity = new Vector3(0, 0, 0);
        isGrouded = false;
        crystals = 0;
        //reset power up
        this.gameObject.GetComponent<PowerUpManager>().SetStatus(0);
        //Debug.Log($"Scene Loaded: Mode - {mode}");
        spawnData = GameObject.FindWithTag("spawn").GetComponent<SpawnScript>();
        spawn = GameObject.FindWithTag("spawn");
       // Debug.Log("Spawn_Info-Pulled" + $" - {spawn}");
        ball.transform.position = spawn.transform.position;
       // Debug.Log(spawnData.useCrystals);


    }

    // Update is called once per frame
    void Update()
    {


    }

    ///These Are The Iputs For Mobile Touch
    public void MoveInput(Vector2 newMoveDir) {   move = newMoveDir; }

    public void LookInput(Vector2 newLookDir) { look = newLookDir; }

    public void JumpInput()
    {
        OnJump();
    }
    
    
 
    public void SprintInput(bool ifTrue)
    {
        if(ifTrue) { stopping = 1f; }
        else { stopping = 0f; }
    }

    /// -----------
   
    
    
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
        menu.Exit();   
    }

    private void MovePlayer()
    {
        lookingTransform = (forwardObject.transform.forward * move.y + forwardObject.transform.right * move.x);
       // Debug.Log(lookingTransformNoY);
        ball.AddForce(lookingTransform * 1000 * Time.deltaTime);
    }

    public void OnJump()
    {
        Debug.Log("Pressed Space");
        if (isOnWall)
        {

            ball.AddForce(wallOutVector * 10 + new Vector3(0, 7, 0), ForceMode.VelocityChange);

            //Plays the wallJump sound effect from audio manager
            audioMngr.PlayActionSound("wallJump");

        }
        else if (isGrouded)
        {
            
            ball.AddForce(0, 7, 0, ForceMode.VelocityChange);

            Debug.Log(audioMngr);
            Debug.Log(audioMngr.transform.gameObject);
            //Plays the jump sound effect from audio manager
            audioMngr.PlayActionSound("jump");

        }
        else if (doubleJump && status == 1) 
        {
            doubleJump = false;
            ball.velocity = new Vector3(ball.velocity.x, 0, ball.velocity.z);
            ball.AddForce(0, 9, 0, ForceMode.VelocityChange);

        }

        if(cannon != null) { cannon.Launch(); }
    }

    public void OnInteract()
    {
        switch(status)
        {
            case 2:
                TryInteract();
                break;

                default:
                Debug.Log("No Activatable Powerup");
                break;

        }
    }

    public GameObject targetObj;
   
    public void TryInteract()
    {
        if (targetObj != null)
        {
            if (targetObj.TryGetComponent(out IEnteractable target))
            {
                //calls the interact action on the object
                target.Interact();

                //Plays the interact sound effect from audio manager
                audioMngr.PlayActionSound("interact");

            }
            else
            {
               // Debug.Log("cannotPickupObj");
            }
        }
        else
        {
          //  Debug.Log("No Object To Pick Up");
        }
    }

    public void TryTargetReset()
    {
        if (targetObj != null)
        {
            if (targetObj.TryGetComponent(out IEnteractable target))
            {
                target.Reset();
            }
            else
            {
                Debug.Log("cannotReset Obj");
            }
        }
        else
        {
            Debug.Log("No Object To Pick Up");
        }
    }

    public void OnReset()
    {
       
        Death();
        
        
    }

    private void FixedUpdate()
    {
       
        
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "lava")
        { Death(); }

        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Floor")
        {
            isGrouded = true;
            doubleJump = true;
        }

        if (collision.gameObject.tag == "Wall" && Detector.triggerOnWall == true)
        {
           // Debug.Log("Touching Wall");
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
        if (collision.gameObject.tag == "Wall") Debug.Log("Collider Left Wall");



    }

    public MenuScript menu;
    private IEnumerator Ahaha()
    {
        if(spawnData.useCrystals)
        {
            if (crystals >= spawnData.crystalsInLevel) 
            {
                ball.velocity = new Vector3(0, 0, 0);
;                Debug.Log("Level Complete");
                menu.StopTimer();
                status = 0;
                DontDestroyOnLoad(this.transform.parent.gameObject);
                shop.BankCrystals(crystals);
                if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 3)
                {
                    menu.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    menu.LoadScene(13);
                }
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
                shop.BankCrystals(crystals);
                menu.StopTimer();


                if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 3)
                {
                    menu.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(13);
                }
                StopAllCoroutines();
                yield return null;
            }
        }
    }

     
    

    void Death()
    {
        if(status == 2) 
        {
            
            TryTargetReset();
            targetObj = null;
        }
        
        ball.constraints = RigidbodyConstraints.FreezeAll;
        if (cannon != null) { cannon = null; this.transform.parent.parent = null; DontDestroyOnLoad(this.transform.parent); }
        Checkpoint.isDead = true;
       
        
        ball.velocity = new Vector3(0, 0, 0);
        ball.angularVelocity = new Vector3(0, 0, 0);
        ball.constraints = RigidbodyConstraints.None;
        ball.transform.position = spawn.transform.position;
        
    }

    
    //sets everything while loading next scene
    public IEnumerator DoNotDestroy(int Scene)
    {
        isOnWall = false;
        
        DontDestroyOnLoad (this.transform.parent.gameObject);

        menu.ResumeGame();
        SceneLoader.FinishLoading(Scene);

        yield return null;
    }
    
}
