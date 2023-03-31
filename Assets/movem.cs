using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movem : MonoBehaviour
{

    private Rigidbody ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) ball.AddForce(new Vector3(0, 0, 1));
        if (Input.GetKey(KeyCode.S)) ball.AddForce(new Vector3(0, 0, -1));
        if (Input.GetKey(KeyCode.A)) ball.AddForce(new Vector3(-1, 0, 0));
        if (Input.GetKey(KeyCode.D)) ball.AddForce(new Vector3(1, 0, 0));

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
        if (collided.gameObject.name == "lava") ball.transform.position = new Vector3(0, 1, 0); ball.velocity = new Vector3(0, 0, 0);
    }



    private IEnumerator Ahaha()
    {
        Debug.Log("WIINNNER");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Win");
        yield return null;
    }
}
