using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    float camDegree;
    GameObject camFollower;
    Vector3 angles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) camDegree++;
        if (Input.GetKey(KeyCode.RightArrow)) camDegree--;
        angles = new Vector3(15, camDegree, 0);
    }

    private void LateUpdate()
    {
      transform.eulerAngles = angles;
    }
}
