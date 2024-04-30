using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private GameObject primaryParticles, secondaryParticles, crossSection;
    [SerializeField] private bool isCrossSection;

    public GameObject GetPrimaryParticles()
    {
        return primaryParticles;
    }

    public GameObject GetSecondaryParticles()
    {
        return secondaryParticles;
    }

    public GameObject GetCrossSection()
    {
        return crossSection;
    }

    public bool IsCrossSection()
    {
        return isCrossSection;
    }
}
