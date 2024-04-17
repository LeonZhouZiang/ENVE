using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private GameObject primaryParticles, secondaryParticles;

    public GameObject GetPrimaryParticles()
    {
        return primaryParticles;
    }

    public GameObject GetSecondaryParticles()
    {
        return secondaryParticles;
    }
}
