using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePhysicsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ParticleMove>())
        {
            other.gameObject.GetComponent<ParticleMove>().DisableFloatingPhysics();
        }
    }
}
