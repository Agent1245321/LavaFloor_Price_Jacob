using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggly : MonoBehaviour
{


    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private static float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle += 1f;
        scaleX = 1 + (Mathf.Cos(angle * Mathf.PI / 180f)) / 10f;
        scaleY = 1 + (Mathf.Sin(angle * Mathf.PI / 180f)) / 10f;
        scaleZ = 1 + (Mathf.Sin(angle * Mathf.PI / 180f + 90))/ 10f;
    }

    private void LateUpdate()
    {
        this.gameObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
