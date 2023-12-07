using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorTrigger : MonoBehaviour
{

    Conveyor conv;
    public void Start()
    {
        conv = transform.parent.GetComponent<Conveyor>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name != "PBall")
        {
            conv.Move(other.gameObject);
        }
        else
        {
            conv.Move(other.transform.parent.gameObject);
        }
    }
}
