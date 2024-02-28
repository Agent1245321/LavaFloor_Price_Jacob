using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceExclusive : MonoBehaviour
{
    public int device;

    void Start()
    {
#if UNITY_WSA
    Check(1);
#endif

#if (UNITY_ANDROID || UNITY_IOS)
        Check(0);
#endif
    }

    public void Check(int d)
    {
        if (d != device)
        {
            Destroy(this.gameObject);
        }
    }
}
