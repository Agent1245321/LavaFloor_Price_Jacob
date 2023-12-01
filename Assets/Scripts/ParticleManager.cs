using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ParticleManager : MonoBehaviour
{
    
    private ParticleSystem pS;

    private void Start()
    {
        pS = GetComponent<ParticleSystem>();
        Play();
    }
    public void Play()
    {
        pS.Play();
    }

    public void Stop()
    {
        pS.Stop();
    }

    public void Pause()
    {
        pS.Pause();
    }
}
