using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClumping : MonoBehaviour
{
    private List<Particle> _particlesInside = new List<Particle>();

    private void OnTriggerEnter(Collider other)
    {
        Particle particle = other.GetComponentInParent<Particle>();
        if (particle == null || _particlesInside.Contains(particle))
        {
            return;
        }
        _particlesInside.Add(particle);
    }

    private void OnTriggerExit(Collider other)
    {
        Particle particle = other.GetComponentInParent<Particle>();
        if (particle == null)
        {
            return;
        }
        _particlesInside.Remove(particle);
    }

    private void FixedUpdate()
    {
        // Iterate through each particle
        foreach (Particle particle in _particlesInside)
        {
            if (!particle.attractToSimilar)
            {
                return;
            }

            Vector3 averageNearbyPos = Vector3.zero;
            foreach (Particle nearbyParticle in particle.nearbyParticles)
            {
                if (nearbyParticle.gameObject.tag != particle.gameObject.tag)
                {
                    continue;
                }
                averageNearbyPos += nearbyParticle.transform.position;
            }
            if (particle.nearbyParticles.Count > 0)
            {
                averageNearbyPos /= particle.nearbyParticles.Count;
                particle.rb.AddForce(((averageNearbyPos - particle.transform.position).normalized * particle.attractionStrength) / Mathf.Clamp((averageNearbyPos - particle.transform.position).magnitude, 1, 100) * particle.nearbyParticles.Count);
            }
        }
    }
}