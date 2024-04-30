using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour , IEnteractable
{
    private Movement movement;
    public Rigidbody obj;
    private GameObject forwardObj;
    bool isPickUped = false;
    public float HoverDistance;
    public Vector3 rotation;
    public Vector3 spawn;

    






    public void Start()
    {
        spawn= transform.position;
        movement = GameObject.FindWithTag("player").GetComponent<Movement>();
        obj = this.transform.GetComponent<Rigidbody>();
        forwardObj = GameObject.FindWithTag("player").transform.parent.Find("ForwardObject").gameObject;
    }


    public virtual void Interact()
    {
        Grab();
    }

    public void Grab()
    {
        if (isPickUped)
        {
            this.transform.position = movement.gameObject.transform.position + new Vector3(0, HoverDistance, 0);
            obj.velocity = (forwardObj.transform.forward * 10) + movement.GetComponent<Rigidbody>().velocity;
            obj.useGravity = true;
            this.transform.GetComponent<Collider>().enabled = true;
        }
        else
        {
            this.transform.position = movement.gameObject.transform.position + new Vector3(0, 1.3f * HoverDistance, 0);
            this.transform.GetComponent<Collider>().enabled = false;
            obj.useGravity = false;
        }

        isPickUped = !isPickUped;
    }


    public virtual void Update()
    {

        Move();
        
    }

    public void Move()
    {
        if (isPickUped)
        {
            this.transform.position = movement.transform.position + new Vector3(0, HoverDistance, 0);
            movement.targetObj = this.gameObject;
            this.transform.eulerAngles = (forwardObj.transform.eulerAngles + rotation);
        }
    }

    public void OnTriggerStay(Collider player)
    {
        //Debug.Log(player.name + "Has Entered Range");


        if (player.tag == "BallTrigger")
        {
            Debug.Log(movement);
            movement.targetObj = this.gameObject;

        }
    }

    public void OnTriggerExit(Collider player)
    {
        //Debug.Log(player.name + "Has Left Range");

        if (player.tag == "BallTrigger")
        {
            //Debug.Log("Out Of Range");
            movement.targetObj = null;
        }
    }

    public bool destroyable;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "lava" && destroyable)
        {
            Reset();
        }
    }

    public void Reset()
    {
        isPickUped = false;
        obj.velocity = Vector3.zero;
        obj.useGravity = true;
        this.transform.GetComponent<Collider>().enabled = true;
        this.transform.position = spawn;
        
    }
}
