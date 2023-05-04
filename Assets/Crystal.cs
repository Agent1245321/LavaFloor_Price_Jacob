using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject crystal;

    public void Start()
    {
        Debug.Log("AM I EVEN STARTING?!");
        
        crystal = this.transform.Find("prism").gameObject;
        Debug.Log(crystal.name);
        particles = this.transform.root.GetComponentInChildren<ParticleSystem>();
        particles.Pause();
        Debug.Log(particles);
    }

    public IEnumerator DestroySelf()
    {
        Debug.Log("Is starting");
        crystal.SetActive(false);
        particles.Play();
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        yield return null;
    }
}
