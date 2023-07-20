using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    float camDegree;
    float camDegreeY;
    public GameObject camFollower;
    private Vector2 look;
    Vector3 angles;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        look = Vector3.ClampMagnitude(Movement.look, 1.0f);
       camDegree += look.x * Time.deltaTime * sensitivity;
       camDegreeY += -look.y * Time.deltaTime * sensitivity;
        if (camDegreeY > 90) camDegreeY = 89.9f;
        if(camDegreeY < -90) camDegreeY = -89.9f;
        angles = new Vector3(camDegreeY, camDegree, 0);

    }

    private void LateUpdate()
    {
      transform.eulerAngles = angles;
      this.transform.position = new Vector3(camFollower.transform.position.x, camFollower.transform.position.y, camFollower.transform.position.z);
    }
}
