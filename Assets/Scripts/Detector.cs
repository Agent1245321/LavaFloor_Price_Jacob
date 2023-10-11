using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Movement move;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.root.GetComponentInChildren<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Wall")
        {
            move.isOnWall = false;
            Debug.Log($"Exiting Trigger from {other.name}");
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        { Debug.Log(other.name); }
    }
}
