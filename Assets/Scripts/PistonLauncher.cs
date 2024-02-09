using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonLauncher : MonoBehaviour
{
    public float pistonForce;
    // Start is called before the first frame update
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "piston")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-this.transform.up * pistonForce);
        }
    }
}
