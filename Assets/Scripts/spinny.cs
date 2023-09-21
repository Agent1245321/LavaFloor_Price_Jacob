using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinny : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       Vector3 rotation = new Vector3(0, 100, 0) * Time.deltaTime;
        transform.Rotate(rotation);
    }
}
