using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;
    [SerializeField] private Vector3 spawnpoint;
    [SerializeField] private float randomness = .5f;
    [SerializeField] private int numberOfParticles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject p = new GameObject();
        p.name = "ParticleParent";
        if (numberOfParticles > 0)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                GameObject c = Instantiate(particles[Random.Range(0, particles.Length)], spawnpoint + new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness), Random.Range(-randomness, randomness)), Quaternion.identity);
                c.transform.parent = p.transform;
            }
        }
    }
}
