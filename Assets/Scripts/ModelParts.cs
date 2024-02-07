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

    public Material transparent;
    public Material defaultMaterial;
    [Header("Components")]
    public GameObject bubbles;
    public GameObject[] WWTPWaterfalls;
    public GameObject[] WTPWaterfalls;
    [Header("Animations")]
    public Animator contactChloroTankWaterAnimation;

    public void Start()
    {
        
    }

    public void ChangeMaterial(int i)
    {
        if (WWTP)
        {
            foreach(var mat in WWTPModel[i].GetComponentsInChildren<MeshRenderer>())
            {
                if (!mat.gameObject.CompareTag("NoneTransparent"))
                    mat.material = transparent;
            }
        }
        else
        {
            foreach (var mat in WTPModel[i].GetComponentsInChildren<MeshRenderer>())
            {
                if(!mat.gameObject.CompareTag("NoneTransparent"))
                    mat.material = transparent;
            }
        }
        CheckBubble(i);
        CheckWaterfall();
    }

    public void RestoreMaterials()
    {
        if (WWTP)
        {
            foreach(var part in WWTPModel)
            {
                foreach (var mat in part.GetComponentsInChildren<MeshRenderer>())
                {
                    if (!mat.gameObject.CompareTag("NoneTransparent"))
                        mat.material = defaultMaterial;
                }
            }
        }
        else
        {
            foreach (var part in WTPModel)
            {
                foreach (var mat in part.GetComponentsInChildren<MeshRenderer>())
                {
                    if (!mat.gameObject.CompareTag("NoneTransparent"))
                        mat.material = defaultMaterial;
                }
            }
        }
    }

    public void CheckBubble(int i)
    {
        if(WWTP && i == 2)
        {
            bubbles.SetActive(true);
        }
        else
        {
            bubbles.SetActive(false);
        }
    }

    public void CheckWaterfall()
    {
        if (WWTP)
        {
            foreach(var o in WWTPWaterfalls)
            {
                o.SetActive(true);
            }
            foreach(var o in WTPWaterfalls)
            {
                o.SetActive(false);
            }
        }
        else
        {
            foreach (var o in WWTPWaterfalls)
            {
                o.SetActive(false);
            }
            foreach (var o in WTPWaterfalls)
            {
                o.SetActive(true);
            }
        }
    }

    public void PlayAnimation(int index)
    {
        if (!WWTP)
        {
            switch (index)
            {
                case 6:
                    contactChloroTankWaterAnimation.Play("chloro tank water flow", 0, 0f);
                    break;

            }

        }
    }
}
