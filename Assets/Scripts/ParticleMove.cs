using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    [SerializeField] private float bouyancy = 10, drag = .95f, waterHeight = 1.5f;
    [SerializeField] private Vector2 flowForce = Vector2.zero;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Particle p = other.gameObject.GetComponent<Particle>();
        if (rb && p && p.PhysicsAreEnabled())
        {
            rb.velocity *= drag;
            if (other.transform.position.y < waterHeight)
            {
                rb.AddForce(new Vector3(flowForce.x, bouyancy, flowForce.y));
            }
        }
    }
}
