using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;
    [SerializeField] private Vector3 origin;
    [SerializeField] private int numberOfParticles;
    [SerializeField] private float minRadius, maxRadius;
    private GameObject p;
    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        p = new GameObject();
        p.name = "ParticleParent";
    }

    public void ChangeParticleState()
    {
        if (!spawned)
        {
            if (numberOfParticles > 0)
            {
                for (int i = 0; i < numberOfParticles; i++)
                {
                    
                    Vector3 spawnpoint = origin + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(minRadius, maxRadius);
                    GameObject c = Instantiate(particles[Random.Range(0, particles.Length)], spawnpoint, Quaternion.identity);
                    c.transform.parent = p.transform;
                }
            }
            spawned = true;
        }
        else
        {
            if (p.activeSelf)
            {
                p.SetActive(false);
            }
            else
            {
                p.SetActive(true);
            }
        }
    }

    public void DisableParticles()
    {
        if (spawned && p.activeSelf)
        {
            p.SetActive(false);
        }
    }
}
