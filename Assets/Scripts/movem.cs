using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movem : MonoBehaviour
{
    //public AudioSource lavaSound;
    public AudioSource deathSound;
    private Rigidbody ball;
    int xValue;
    int yValue;
    int zValue;
    Wiggly wiggly;
    bool isGrouded;
    bool isOnWall;
    GameObject wall;

    Vector3 lookingTransform;
    public GameObject cam;

    bool stopping;
    // Start is called before the first frame update
    void Start()
    {
        ball = this.GetComponent<Rigidbody>();

        

    }

    // Update is called once per frame
    void Update()
    {
        lookingTransform = (cam.transform.forward.normalized * yValue) + (cam.transform.right.normalized * xValue);
        if (Input.GetKey(KeyCode.W)) yValue = 1;
        else if (Input.GetKey(KeyCode.S)) yValue = -1;
        else yValue = 0;
        if (Input.GetKey(KeyCode.A)) xValue = -1;
        else if (Input.GetKey(KeyCode.D)) xValue = 1;
        else xValue = 0;

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        

        stopping = Input.GetKey(KeyCode.LeftShift) ? true : false;

    }

    private void FixedUpdate()
    {
        ball.AddForce(lookingTransform * 1000 * Time.deltaTime);
        //lavaSound.volume = 1 / this.transform.position.y;
        Debug.Log(isGrouded);
        Debug.Log(isOnWall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "win")
        {
            ball.AddForce(new Vector3(0, 1000, 0), ForceMode.VelocityChange); 
            StartCoroutine(Ahaha());
        }

        
    }

    private void OnCollisionEnter(Collision collided)
    {
        if (collided.gameObject.name == "lava")
        { ball.transform.position = new Vector3(0, 2, 0); deathSound.Play(); ball.velocity = new Vector3(0, 0, 0); }
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") isGrouded = true;
        if (collision.gameObject.tag == "Wall") isOnWall = true;
        wall = collision.gameObject;
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
        Debug.Log("WIINNNER");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Win");
        yield return null;
    }

     void Jump()
    {
        if(isOnWall)
        {
            if(ball.transform.position.x - wall.transform.position.x > 0)
            { ball.AddForce(7, 5, 0, ForceMode.VelocityChange); }
            else if (ball.transform.position.x - wall.transform.position.x < 0)
            {
                ball.AddForce(-7, 5, 0, ForceMode.VelocityChange);
            }
            
        }
       else if(isGrouded)
        {
            ball.AddForce(0, 5, 0, ForceMode.VelocityChange);
        }

    }
}
