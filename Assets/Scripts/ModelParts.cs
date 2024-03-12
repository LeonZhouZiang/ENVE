using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelParts : MonoBehaviour
{
    public static ModelParts Instance;
    public bool WWTP = false;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] WWTPModel;
    public GameObject[] WTPModel;

    [Header("Components")]
    public GameObject bubbles;
    public GameObject[] WWTPWaterfalls;
    public GameObject[] WTPWaterfalls;
    [Header("Animations")]
    public Animator contactChloroTankWaterAnimation;

    public void Start()
    {
        
    }

    public void ChangeMaterial(int index)
    {
        if (WWTP)
        {
            for(int i = 0; i < WWTPModel.Length; i++)
            {
                WWTPModel[i].layer = LayerMask.NameToLayer("Outline");
            }
        }
        else
        {
            for (int i = 0; i < WTPModel.Length; i++)
            {
                WTPModel[i].layer = LayerMask.NameToLayer("Outline");
            }
        }

    }

    public void RestoreMaterials()
    {
        if (WWTP)
        {
            for (int i = 0; i < WWTPModel.Length; i++)
            {
                WWTPModel[i].layer = LayerMask.NameToLayer("Default");
            }
        }
        else
        {
            for (int i = 0; i < WTPModel.Length; i++)
            {
                WTPModel[i].layer = LayerMask.NameToLayer("Default");
            }
        }
    }


    public void PlayAnimation(int index)
    {
        
    }
}
