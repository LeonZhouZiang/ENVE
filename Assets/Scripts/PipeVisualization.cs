using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeVisualization : MonoBehaviour
{
    [SerializeField] private GameObject flowSphere;
    private GameObject fSphere, nextPipe = null;
    private bool startingPipe;
    private LineRenderer l;
    private float moveSpeed = 10;
    private int group, section, pipeNumber;

    // Start is called before the first frame update
    void Start()
    {
        nextPipe = Initialize();

        l = GetComponent<LineRenderer>();
        fSphere = Instantiate(flowSphere);
        fSphere.SetActive(false);
        StartCoroutine(MoveFlowSphere());
    }

    private GameObject Initialize()
    {
        int idNumber = int.Parse(gameObject.name);
        pipeNumber = idNumber % 100;
        section = (idNumber / 100) % 100;
        group = idNumber / 10000;

        string potentialNextPipe;
        potentialNextPipe = ((("" + group) + section) + (pipeNumber + 1));
        if (GameObject.Find(potentialNextPipe))
        {
            return GameObject.Find(potentialNextPipe);
        }



        return null;
    }

    private IEnumerator MoveFlowSphere()
    {
        while (true)
        {
            float leftover = 0;
            for (int i = 0; i < l.positionCount - 1; i++)
            {
                Vector3 pos1 = transform.TransformDirection(l.GetPosition(i) * transform.localScale.y) + transform.position;
                Vector3 pos2 = transform.TransformDirection(l.GetPosition(i + 1) * transform.localScale.y) + transform.position;
                float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
                float distanceTraveled = leftover;
                for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
                {
                    fSphere.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled/distanceBetweenPositions);
                    yield return null;
                }
                leftover = distanceTraveled - distanceBetweenPositions;
            }
        }
    }

    public void ToggleVisuals()
    {
        fSphere.SetActive(!fSphere.activeSelf);
    }
}
