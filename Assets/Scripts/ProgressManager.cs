using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public bool WWTP = false;
    public int progress = 0;
    public GameObject currentCam;

    public Transform[] cameraPositionWWTP;
    public Transform[] cameraPositionWTP;

    public GameObject[] WWTPCameras;
    public GameObject[] WTPCameras;

    public TextMeshProUGUI description;

    public GameObject WWTPButtons;
    public GameObject WTPButtons;

    public GameObject WWTPModel;
    public GameObject WTPModel;
    void Start()
    {
        currentCam = Camera.main.gameObject;
        Starting();
    }

    void Starting()
    {
        if (WWTP)
        {
            ModelParts.Instance.WWTP = true;
           
            WWTPModel.SetActive(true);
            WWTPButtons.SetActive(true);

            WTPModel.SetActive(false);
            WTPButtons.SetActive(false);

            //currentCam.transform.SetPositionAndRotation(cameraPositionWWTP[0].position, cameraPositionWWTP[0].rotation);
            Debug.Log(cameraPositionWWTP[0].position);
        }
        else
        {
            ModelParts.Instance.WWTP = false;
            WTPModel.SetActive(true);
            WTPButtons.SetActive(true);

            WWTPModel.SetActive(false);
            WWTPButtons.SetActive(false);

            //currentCam.transform.SetPositionAndRotation(cameraPositionWTP[0].position, cameraPositionWTP[0].rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (WWTP)
        //    mainCamera.transform.position = cameraPositionWTP[progress].position;
        //else
        //    mainCamera.transform.position = cameraPositionWWTP[progress].position;
    }

    public void SwitchModels()
    {
        description.text = "";
        if (WWTP)
        {
            ModelParts.Instance.WWTP = false;
            WWTP = false;
            WWTPModel.SetActive(false);
            WWTPButtons.SetActive(false);

            WTPModel.SetActive(true);
            WTPButtons.SetActive(true);

            currentCam.transform.SetPositionAndRotation(cameraPositionWTP[0].position, cameraPositionWTP[0].rotation);
        }
        else
        {
            ModelParts.Instance.WWTP = true;
            WWTP = true;
            WTPModel.SetActive(false);
            WTPButtons.SetActive(false);

            WWTPModel.SetActive(true);
            WWTPButtons.SetActive(true);

            currentCam.transform.SetPositionAndRotation(cameraPositionWWTP[0].position, cameraPositionWWTP[0].rotation);
        }
    }

    #region WWTP

    public void WWTP0()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(0);

        progress = 0;
        SmoothCamera(cameraPositionWWTP[progress].position, cameraPositionWWTP[progress].rotation);
        description.text = "Bar grit chamber: used to remove large objects and dense inorganic particles(sand, glass, debris) present in raw wastewater.Such objects can damage parts screens.";
    }

    public void WWTP1()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(1);

        progress = 1;
        SmoothCamera(cameraPositionWWTP[progress].position, cameraPositionWWTP[progress].rotation);
        description.text = "Primary Settling Tank: used to remove particles that either settle or float using mechanical scrapers and pumps.At the bottom of the tank, primary sludge (waste stream) is collected and routed to the biosolids processing system(shown as the waste container in the schematic).";
    }

    public void WWTP2()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(2);

        progress = 2;
        SmoothCamera(cameraPositionWWTP[progress].position, cameraPositionWWTP[progress].rotation);
        description.text = "Aeration Tank: air (oxygen) is added to this tank at the bottom (using bubble diffusers) to provide adequate mixing of primary effluent and activated sludge(called mixed liquor) and supply oxygen needed for microbes to break downorganic matter & grow.This is a biological process performed by aerobic ";
    }

    public void WWTP3()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(3);

        progress = 3;
        SmoothCamera(cameraPositionWWTP[progress].position, cameraPositionWWTP[progress].rotation);
        description.text = "Secondary Settling Tank: the activated sludge containing large microbial biomass is settled in this tank.Part of the flow is recycled to the aeration tank(RAS ¨C recycled activated sludge) to provide sufficient microbial population whilethe remaining is collected and sent to the biosolids processing system(WAS ¨Cwaste activated sludge).";
    }

    public void WWTP4()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(4);

        progress = 4;
        SmoothCamera(cameraPositionWWTP[progress].position, cameraPositionWWTP[progress].rotation);
        description.text = "UV Disinfection: pathogenic microbes remaining in treated water are inactivated using high-energy lamps that emit UV radiation.The water flows perpendicular tolongitudinal UV lamps. The UV photons damage the genetic material of virusesand other microbes present in wastewater.";
    }
    #endregion

    #region WTP
    public void WTP0()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(0);

        progress = 0;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Storage tank for coagulant chemicals: used to store aluminum sulfate (alum), iron(II) sulfate, or other chemical coagulant. It will be mixed with source waterinside the coagulation tank.";
    }

    public void WTP1()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(1);

        progress = 1;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Coagulation tank: tank where the coagulant chemical(clarifying agent) will be rapidly mixed with the source water for a short period using a motor andpropeller.Small flocs start to form by forcing small particles to stick together(viachemical and physical actions) with the aid of the coagulant.";
    }

    public void WTP2()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(2);

        progress = 2;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Flocculation tank: slow and gentle mixing tank where particles & small flocs are allowed to collide, adhere to one another, and grow to large flocs. The watertravels through a baffled tank system and stays in this tank for a relatively longperiod to promote sufficient flocculation(floc growth).";
    }

    public void WTP3()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(3);

        progress = 3;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Sedimentation tank (Horizontal flow clarifier): this is a settling tank where the large flocs from the flocculation tank settle out at the bottom(sludge zone).Settled flocs are removed periodically using a scraper arm(not shown) andsludge collection trough. Water flows slowly here and resides in the tank for > 3hrs.";
    }

    public void WTP4()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(4);

        progress = 4;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Filtration tanks: water flows slowly from the top of the tank to the bottom through a bed of granular porous media(not shown) & particles get trapped dueto physical separation(adhesion, interception &straining).";
    }

    public void WTP5()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(5);

        progress = 5;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Chlorine Chemical Storage Tank: this is used to store disinfectant chemicals(typically chlorine).Chlorine is a toxic gas and should be carefully stored.";
    }

    public void WTP6()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(6);
        //ModelParts.Instance.PlayAnimation(6);

        progress = 6;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Chlorination Contact Tank: the water from the filtration tank is mixed with chlorine disinfectant inside this baffled tank. Chlorine is a strong disinfectant thatkills pathogenic microbes(such as bacteria and viruses) present in water for safehuman consumption.";
    }

    public void WTP7()
    {
        ModelParts.Instance.RestoreMaterials();
        ModelParts.Instance.ChangeMaterial(7);

        progress = 7;
        SmoothCamera(cameraPositionWTP[progress].position, cameraPositionWTP[progress].rotation);
        description.text = "Storage tank: finished clean water is stored at the end of the treatment process nbefore distribution to the local community.";
    }
    #endregion

    public float cameraSmoothTime = 2;
    void SmoothCamera(Vector3 position, Quaternion rotation)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCamera(position, rotation, cameraSmoothTime));
    }

    IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        float time = 0;
        Vector3 startPosition = currentCam.transform.position;
        Quaternion startRotation = currentCam.transform.rotation;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            currentCam.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            currentCam.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        currentCam.transform.position = targetPosition;
        currentCam.transform.rotation = targetRotation;
    }

    void SwitchCamera(int index)
    {
        if (WWTP && index < WWTPCameras.Length)
        {
            currentCam.SetActive(false);

            WWTPCameras[index].SetActive(true);
            currentCam = WWTPCameras[index];
        }
        else if(!WWTP && index < WTPCameras.Length)
        {
            currentCam.SetActive(false);

            WTPCameras[index].SetActive(true);
            currentCam = WTPCameras[index];
        }
    }
}
