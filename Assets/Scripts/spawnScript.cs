using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public int crystalsInLevel;
    public Vector3 spawn;
    public bool useCrystals;

    // Start is called before the first frame update
    void Awake()
    {
        spawn = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
