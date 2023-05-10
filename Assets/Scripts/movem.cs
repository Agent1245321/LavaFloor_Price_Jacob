using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movem : MonoBehaviour
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
    private spawnScript spawnData;
    private bool collectedWin = false;

    bool stopping;
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
        spawnData = GameObject.FindWithTag("spawn").GetComponent<spawnScript>();
        spawn = GameObject.FindWithTag("spawn").transform.position;
        Debug.Log("Spawn_Info-Pulled" + $" - {spawn}");
        ball.transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        lookingTransform = (cam.transform.forward.normalized * yValue) + (cam.transform.right.normalized * xValue);
        lookingTransformNoY = new Vector3(lookingTransform.x, 0, lookingTransform.z);
        if (Input.GetKey(KeyCode.W)) yValue = 1;
        else if (Input.GetKey(KeyCode.S)) yValue = -1;
        else yValue = 0;
        if (Input.GetKey(KeyCode.A)) xValue = -1;
        else if (Input.GetKey(KeyCode.D)) xValue = 1;
        else xValue = 0;

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.Backspace)) { Death(); }


        stopping = Input.GetKey(KeyCode.LeftShift) ? true : false;

    }

    private void FixedUpdate()
    {
        ball.AddForce(lookingTransformNoY * 1000 * Time.deltaTime);
        lavaSound.volume = 10 / this.transform.position.y; 
        
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
        if (collision.gameObject.tag == "Floor") isGrouded = true;
        if (collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
            wallTouchPoint = collision.GetContact(0).point;
            wallOutVector = (ball.transform.position - wallTouchPoint).normalized;
            Debug.Log(wallOutVector);
        }
        if (stopping)
        {
            ball.velocity = new Vector3(ball.velocity.x / 1.1f, ball.velocity.y, ball.velocity.z / 1.1f);
        }
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

     void Jump()
    {
        
        if(isOnWall)
        {

            ball.AddForce(wallOutVector * 10 + new Vector3(0, 7, 0), ForceMode.VelocityChange);
            
        }
       else if(isGrouded)
        {
            ball.AddForce(0, 7, 0, ForceMode.VelocityChange);
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
