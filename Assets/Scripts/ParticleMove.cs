using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    [SerializeField] private float bouyancy = 10, drag = .95f, waterHeight = 1.5f, bouyancyFactor = 15;
    [SerializeField] private Vector2 flowForce = Vector2.zero;
    [SerializeField] private bool enableJitterReduction = true;

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Particle p = other.gameObject.GetComponent<Particle>();
            if (rb && p && p.PhysicsAreEnabled())
            {
                rb.velocity *= drag;
                if (Mathf.Abs(other.transform.position.y - (waterHeight + .5f)) < .1f && enableJitterReduction)
                {
                    rb.AddForce(new Vector3(flowForce.x, bouyancy * bouyancyFactor * (waterHeight - other.transform.position.y), flowForce.y));
                }
                else if (other.transform.position.y < waterHeight)
                {
                    rb.AddForce(new Vector3(flowForce.x, bouyancy, flowForce.y));
                }
            }
        }
    }
}
