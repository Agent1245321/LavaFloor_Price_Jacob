using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    float camDegree;
    public GameObject camFollower;
    Vector3 angles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) camDegree += 100 * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) camDegree -= 100 * Time.deltaTime;
        angles = new Vector3(25, camDegree, 0);

    }

    private void LateUpdate()
    {
      transform.eulerAngles = angles;
      this.transform.position = new Vector3(camFollower.transform.position.x, camFollower.transform.position.y, camFollower.transform.position.z);
    }
}
