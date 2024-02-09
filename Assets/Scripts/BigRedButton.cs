using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedButton : MonoBehaviour
{
    public GameObject Connection;
    public IEvent connected;
    public Material colorb;
    // Start is called before the first frame update
    public void Start()
    {
        if (Connection.TryGetComponent(out IEvent target))
        {
            connected = target;
        }
        else
        {
            Debug.LogError("Could Not Get IEvent Script");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            Debug.Log("HIT IT");
            connected.IsActive = true;
            this.gameObject.GetComponent<MeshRenderer>().material= colorb;
            connected.Activate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
