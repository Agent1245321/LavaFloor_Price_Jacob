using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    bool isOnWall;
    Vector3 wallTouchPoint;
    Vector3 wallOutVector;
    public int crystals;

    Vector3 lookingTransform;
    Vector3 lookingTransformNoY;
    public GameObject cam;
    private Vector3 spawn;
    private SpawnScript spawnData;
    private bool collectedWin = false;
    private Vector2 move;
    public static Vector2 look;


    private float stopping;
    // Start is called before the first frame update

    private void Awake()
    {
        ball = this.GetComponent<Rigidbody>();
        lavaSound = this.transform.root.Find("GameObject").GetComponentInChildren<AudioSource>();
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
        crystals = 0;
        Debug.Log($"Scene Loaded: Mode - {mode}");
        spawnData = GameObject.FindWithTag("spawn").GetComponent<SpawnScript>();
        spawn = GameObject.FindWithTag("spawn").transform.position;
        Debug.Log("Spawn_Info-Pulled" + $" - {spawn}");
        ball.transform.position = spawn;
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
        Debug.Log(Vector3.ClampMagnitude(look.normalized, 1.0f));
    }
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        
        
    }

    private void MovePlayer()
    {
        lookingTransform = (cam.transform.forward.normalized * move.y) + (cam.transform.right.normalized * move.x);
        lookingTransformNoY = new Vector3(lookingTransform.x, 0, lookingTransform.z);
        ball.AddForce(lookingTransformNoY * 1000 * Time.deltaTime);
    }

    public void OnJump()
    {

        if (isOnWall)
        {

            ball.AddForce(wallOutVector * 10 + new Vector3(0, 7, 0), ForceMode.VelocityChange);

        }
        else if (isGrouded)
        {
            ball.AddForce(0, 7, 0, ForceMode.VelocityChange);
        }

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

        
    }

    private void OnCollisionEnter(Collision collided)
    {
        if (collided.gameObject.tag == "lava")
        { Death(); }
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall") isGrouded = true;
        if (collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
            wallTouchPoint = collision.GetContact(0).point;
            wallOutVector = (ball.transform.position - wallTouchPoint).normalized;
            Debug.Log(wallOutVector);
        }
       /* if (stopping)
        {
            ball.velocity = new Vector3(ball.velocity.x / 1.1f, ball.velocity.y, ball.velocity.z / 1.1f);
        }
       */
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall") isOnWall = false;
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StopAllCoroutines();
                yield return null;
            }
            
        }
        else
        {
            if(collectedWin)
            {
                ball.velocity = new Vector3(0, 0, 0);
                Debug.Log("Level Complete");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                StopAllCoroutines();
                yield return null;
            }
        }
    }

     
    

    void Death()
    {
        ball.transform.position = spawn;
        deathSound.Play();
        ball.velocity = new Vector3(0, 0, 0);
        ball.angularVelocity = new Vector3(0, 0, 0);
    }

    void LevelComplete()
    {

    }
}
