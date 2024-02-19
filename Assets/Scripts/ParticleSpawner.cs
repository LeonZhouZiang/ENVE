using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private Vector3 spawnpoint;
    [SerializeField] private int numberOfParticles;

    // Start is called before the first frame update
    void Start()
    {
        if (numberOfParticles > 0)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                Instantiate(particle, spawnpoint, Quaternion.identity);
            }
        }
    }
}
