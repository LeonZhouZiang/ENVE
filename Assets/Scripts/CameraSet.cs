using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private GameObject primaryParticles, secondaryParticles, chamber;
    [SerializeField] private bool isCrossSection;

    public GameObject GetPrimaryParticles()
    {
        return primaryParticles;
    }

    public GameObject GetSecondaryParticles()
    {
        return secondaryParticles;
    }

    public GameObject GetChamber()
    {
        return chamber;
    }

    public bool IsCrossSection()
    {
        return isCrossSection;
    }
}
