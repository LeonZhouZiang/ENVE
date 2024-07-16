using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private bool physicsEnabled = true;
    [SerializeField] private bool attractToSimilar;
    [SerializeField] private float attractionStrength = 1;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool PhysicsAreEnabled()
    {
        return physicsEnabled;
    }

    public void EnablePhysics()
    {
        physicsEnabled = true;
    }

    public void DisablePhysics()
    {
        physicsEnabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (attractToSimilar)
        {
            GameObject p = other.gameObject;
            if (p.tag == gameObject.tag)
            {
                Debug.Log(((p.transform.position - transform.position).normalized * attractionStrength) / Mathf.Clamp((p.transform.position - transform.position).magnitude, 1, 100));
                rb.AddForce(((p.transform.position - transform.position).normalized * attractionStrength) / Mathf.Clamp((p.transform.position - transform.position).magnitude, 1, 100));
            }
        }
    }
}
