using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool on = false;
    public float speed = 5;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (on)
        {
            transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
