using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movem : MonoBehaviour
{
    public AudioSource lavaSound;
    public AudioSource deathSound;
    private Rigidbody ball;
    int xValue;
    int yValue;
    // Start is called before the first frame update
    void Start()
    {
        ball = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) yValue = 1;
        else if (Input.GetKey(KeyCode.S)) yValue = -1;
        else yValue = 0;
        if (Input.GetKey(KeyCode.A)) xValue = -1;
        else if (Input.GetKey(KeyCode.D)) xValue = 1;
        else xValue = 0;


    }

    private void FixedUpdate()
    {
        ball.AddForce(new Vector3(xValue, 0, yValue) * 1000 * Time.deltaTime);
        lavaSound.volume = 1 / this.transform.position.y;
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



    private IEnumerator Ahaha()
    {
        Debug.Log("WIINNNER");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Win");
        yield return null;
    }
}
