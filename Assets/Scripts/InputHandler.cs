using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject cameraParent, wwtp, terrain;
    [SerializeField] private Image transitionPanel;
    [SerializeField] private Color transitionColor;
    [SerializeField] private float transitionTime;
    [SerializeField] private Material normalSkyBox, blankSkyBox;
    private int numCams;
    private int currentCam = 0;
    private GameObject[] cams;
    private bool changingCameras;
    private bool terrainEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        numCams = cameraParent.transform.childCount;
        cams = new GameObject[numCams];
        for (int i = 0; i < numCams; i++)
        {
            cams[i] = cameraParent.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!changingCameras)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(ChangeCamera(true));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(ChangeCamera(false));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangePrimaryParticles();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeSecondaryParticles();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Time.timeScale = 5;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Time.timeScale = 1;
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                ToggleTerrain();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePipelines();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleFlowVisuals();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void TogglePipelines()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject p in pipes)
        {
            p.GetComponent<LineRenderer>().enabled = !p.GetComponent<LineRenderer>().enabled;
        }
    }

    private void ToggleFlowVisuals()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject p in pipes)
        {
            TPipeVisualization t;
            PipeVisualization v;
            p.TryGetComponent<TPipeVisualization>(out t);
            p.TryGetComponent<PipeVisualization>(out v);
            if (t)
            {
                //t.ToggleVisuals();
            }
            else if (p)
            {
                //v.ToggleVisuals();
            }
        }
    }

    // True goes to next camera, False goes to previous camera
    private IEnumerator ChangeCamera(bool direction)
    {
        changingCameras = true;

        for (float f = 0; f < transitionTime; f += Time.deltaTime)
        {
            transitionPanel.color = Color.Lerp(Color.clear, transitionColor, f/transitionTime);
            yield return null;
        }
        transitionPanel.color = transitionColor;

        yield return new WaitForSecondsRealtime(.2f);

        cams[currentCam].SetActive(false);

        ReactivateAllChambers();

        int nextCam = 0;

        if (direction)
        {
            nextCam = currentCam + 1;
            if (nextCam >= numCams)
            {
                nextCam = 0;
            }
        }
        else
        {
            nextCam = currentCam - 1;
            if (nextCam < 0)
            {
                nextCam = numCams - 1;
            }
        }

        if (cams[currentCam].GetComponent<CameraSet>().GetParticleSystemID() != cams[nextCam].GetComponent<CameraSet>().GetParticleSystemID())
        {
            GameObject p = cams[currentCam].GetComponent<CameraSet>().GetPrimaryParticles();
            if (p)
            {
                p.GetComponent<ParticleSpawner>().DisableParticles();
            }
            GameObject s = cams[currentCam].GetComponent<CameraSet>().GetSecondaryParticles();
            if (s)
            {
                s.GetComponent<ParticleSpawner>().DisableParticles();
            }
        }

        currentCam = nextCam;

        DisableChambers();

        cams[currentCam].SetActive(true);

        for (float f = 0; f < transitionTime; f += Time.deltaTime)
        {
            transitionPanel.color = Color.Lerp(transitionColor, Color.clear, f/transitionTime);
            yield return null;
        }
        transitionPanel.color = Color.clear;

        changingCameras = false;
    }

    private void ChangePrimaryParticles()
    {
        GameObject p = cams[currentCam].GetComponent<CameraSet>().GetPrimaryParticles();
        if (p)
        {
            p.GetComponent<ParticleSpawner>().ChangeParticleState();
        }
    }

    private void ChangeSecondaryParticles()
    {
        GameObject p = cams[currentCam].GetComponent<CameraSet>().GetSecondaryParticles();
        if (p)
        {
            p.GetComponent<ParticleSpawner>().ChangeParticleState();
        }
    }

    private void ReactivateAllChambers()
    {
        if (cams[currentCam].GetComponent<CameraSet>().IsCrossSection())
        {
            GameObject g = cams[currentCam].GetComponent<CameraSet>().GetCrossSection();
            g.SetActive(false);
        }

        if (terrainEnabled)
        {
            terrain.SetActive(true);
        }

        for (int i = 0; i < wwtp.transform.childCount; i++)
        {
            wwtp.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void DisableChambers()
    {
        if (cams[currentCam].GetComponent<CameraSet>().IsCrossSection())
        {
            GameObject g = cams[currentCam].GetComponent<CameraSet>().GetCrossSection();
            g.SetActive(true);

            terrain.SetActive(false);
            for (int i = 0; i < wwtp.transform.childCount; i++)
            {
                GameObject temp = wwtp.transform.GetChild(i).gameObject;
                wwtp.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void ToggleTerrain()
    {
        if (!cams[currentCam].GetComponent<CameraSet>().IsCrossSection())
        {
            terrainEnabled = !terrainEnabled;
            if (terrainEnabled)
            {
                terrain.SetActive(true);
                RenderSettings.skybox = normalSkyBox;
            }
            else
            {
                terrain.SetActive(false);
                RenderSettings.skybox = blankSkyBox;
            }
        }
    }
}
