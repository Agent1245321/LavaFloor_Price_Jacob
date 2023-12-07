using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public float speed;
    public void Move(GameObject obj)
    {
        obj.transform.Translate(this.transform.right * speed * Time.deltaTime, Space.World);
    }
}
