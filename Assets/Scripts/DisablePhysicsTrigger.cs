using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePhysicsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Particle p = other.gameObject.GetComponent<Particle>();
        if (p)
        {
            p.DisablePhysics();
        }
    }
}
