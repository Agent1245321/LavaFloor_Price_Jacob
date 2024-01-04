using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonBrain : MonoBehaviour
{
    public GameObject cap;
    public GameObject Connection;
    public IEvent connected;
    public bool usePandH;


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
    private void OnTriggerEnter(Collider other)
    {
        if(!usePandH)
        { 
            connected.IsActive = !connected.IsActive; 
        }
        
        if (other.gameObject == cap)
        {
            
            Debug.Log("Button Pressed");
           
            connected.Activate();



        }


    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject == cap && usePandH)
        {
            connected.IsActive = true;
            connected.Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cap && usePandH)
        {
            connected.IsActive = false;
            connected.Activate();
        }
    }
}
