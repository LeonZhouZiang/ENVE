using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject cameraParent;
    [SerializeField] private Image transitionPanel;
    [SerializeField] private Color transitionColor;
    [SerializeField] private float transitionTime;
    private int numCams;
    private int currentCam = 0;
    private GameObject[] cams;
    private bool changingCameras;

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
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

        if (direction)
        {
            currentCam++;
            if (currentCam >= numCams)
            {
                currentCam = 0;
            }
        }
        else
        {
            currentCam--;
            if (currentCam < 0)
            {
                currentCam = numCams - 1;
            }
        }

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
}
