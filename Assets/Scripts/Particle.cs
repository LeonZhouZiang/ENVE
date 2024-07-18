using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private bool physicsEnabled = true;

    [SerializeField] private bool _attractToSimilar;
    public bool attractToSimilar => _attractToSimilar;
    [SerializeField] private float _attractionStrength = 1;
    public float attractionStrength => _attractionStrength;

    private Rigidbody _rb;
    public Rigidbody rb => _rb;

    private List<Particle> _nearbyParticles = new List<Particle>();
    public List<Particle> nearbyParticles => _nearbyParticles;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
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

    private void OnTriggerEnter(Collider other)
    {
        Particle particle = other.GetComponentInParent<Particle>();
        if (particle == null || _nearbyParticles.Contains(particle))
        {
            return;
        }
        _nearbyParticles.Add(particle);
    }

    private void OnTriggerExit(Collider other)
    {
        Particle particle = other.GetComponentInParent<Particle>();
        if (particle == null)
        {
            return;
        }
        _nearbyParticles.Remove(particle);
    }
}