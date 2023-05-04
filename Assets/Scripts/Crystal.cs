using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject crystal;

    public void Start()
    {
        
        
        crystal = this.transform.Find("prism").gameObject;
       
        particles = this.transform.root.GetComponentInChildren<ParticleSystem>();
        particles.Pause();
        
    }

    public IEnumerator DestroySelf()
    {
        
        crystal.SetActive(false);
        particles.Play();
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        yield return null;
    }
}
