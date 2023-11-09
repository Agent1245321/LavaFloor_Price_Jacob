using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamScript : MonoBehaviour
{
    public bool invertY;
    public bool invertX;
    float camDegree;
    float camDegreeY;
    public GameObject camFollower;
    private Vector2 look;
    Vector3 angles;
    public float sensitivity;
    public GameObject forwardObject;

    public int yInv = 1;
    public int xInv = 1;




    // Start is called before the first frame update
    void Start()
    {
        yInv = PlayerPrefs.GetInt("yInv", 1);
        xInv = PlayerPrefs.GetInt("xInv", 1);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

       
       

        

        look = Vector3.ClampMagnitude(Movement.look, 5.0f);
        
        camDegree += look.x * Time.deltaTime * sensitivity * xInv;
        camDegreeY += -look.y * Time.deltaTime * sensitivity * yInv;
        if (camDegreeY > 90) camDegreeY = 89.9f;
        if (camDegreeY < -90) camDegreeY = -89.9f;
        angles = new Vector3(camDegreeY, camDegree, 0);

        forwardObject.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);

    }

    public void UpdateSensitivity(float Sens)
    {
        PlayerPrefs.SetFloat("sensitivity", Sens);
        sensitivity = Sens * 2f;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = angles;
        this.transform.position = new Vector3(camFollower.transform.position.x, camFollower.transform.position.y, camFollower.transform.position.z);
    }

    public void ToggleY()
    {
        yInv = -yInv;
        PlayerPrefs.SetInt("yInv", yInv);

       
       
    }

    public void ToggleX()
    {
       xInv = -xInv;
        PlayerPrefs.SetInt("xInv", xInv);

    }
}
