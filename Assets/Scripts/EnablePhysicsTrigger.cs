using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePhysicsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ParticleMove>())
        {
            other.gameObject.GetComponent<ParticleMove>().EnableFloatingPhysics();
        }
    }
}
