using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLauncher : MonoBehaviour
{
    GameObject player;
    public bool canCollide = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PLAYER" && canCollide)
        {
            player = other.gameObject;
            other.transform.parent.parent = transform;
            other.transform.position = transform.position;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void OnJump()
    {
        if (player != null)
        {
           
            canCollide = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.transform.parent.parent = null;
            player.GetComponent<Rigidbody>().AddForce(transform.parent.forward * -100, ForceMode.VelocityChange);
            StartCoroutine(Count());
            
            player = null;
        }
    }

    public IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        canCollide = true;
        yield return null; 
    }
}
