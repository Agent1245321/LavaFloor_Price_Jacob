using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
  public void ControlTime(float f)
    {
        Time.timeScale = f;
    }
}
