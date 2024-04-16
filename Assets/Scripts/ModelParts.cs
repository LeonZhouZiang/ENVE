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

    public Material outlineMat;
    public float[] WWTPOutline;
    public float[] WTPOutline;

    public void Start()
    {
        
    }

    public void ChangeMaterial(int index)
    {
        if (WWTP)
        {
            outlineMat.SetFloat("Thickness", WWTPOutline[index]);
            for (int j = 0; j < WWTPModel[index].transform.childCount; j++)
            {
                GameObject child = WWTPModel[index].transform.GetChild(j).gameObject;
                if (child.layer != LayerMask.NameToLayer("ExcludeOutline"))
                    child.layer = LayerMask.NameToLayer("Outline");
            }
        }
        else
        {
            outlineMat.SetFloat("Thickness", WTPOutline[index]);
            for (int j = 0; j < WTPModel[index].transform.childCount; j++)
            {
                GameObject child = WTPModel[index].transform.GetChild(j).gameObject;
                if (child.layer != LayerMask.NameToLayer("ExcludeOutline"))
                    child.layer = LayerMask.NameToLayer("Outline");
            }
        }

    }

    public void RestoreMaterials()
    {
        if (WWTP)
        {
            for (int i = 0; i < WWTPModel.Length; i++)
            {
                for (int j = 0; j < WWTPModel[i].transform.childCount; j++)
                {
                    GameObject child = WWTPModel[i].transform.GetChild(j).gameObject;
                    if (child.layer != LayerMask.NameToLayer("ExcludeOutline"))
                        child.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
        else
        {
            for (int i = 0; i < WTPModel.Length; i++)
            {
                for (int j = 0; j < WTPModel[i].transform.childCount; j++)
                {
                    GameObject child = WTPModel[i].transform.GetChild(j).gameObject;
                    if (child.layer != LayerMask.NameToLayer("ExcludeOutline"))
                        child.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }


    public void PlayAnimation(int index)
    {
        
    }
}
