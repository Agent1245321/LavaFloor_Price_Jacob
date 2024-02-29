using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceExclusive : MonoBehaviour
{
    public int device;

    void Start()
    {
        Check(MenuScript.scheme);
    }

    public void Check(int d)
    {
        if (d != device)
        {
            Destroy(this.gameObject);
        }
    }
}
