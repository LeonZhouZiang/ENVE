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

    private void Awake()
    {
        nextPipe = Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
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

        if (pipeNumber == 1)
        {
            startingPipe = true;
        }

        string potentialNextPipe = "";
        if (group < 10)
        {
            potentialNextPipe += 0;
        }
        potentialNextPipe += group;
        if (section < 10)
        {
            potentialNextPipe += 0;
        }
        potentialNextPipe += section;
        if (pipeNumber + 1 < 10)
        {
            potentialNextPipe += 0;
        }
        potentialNextPipe += (pipeNumber + 1);

        return GameObject.Find(potentialNextPipe);
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

    public GameObject GetNextPipe()
    {
        return nextPipe;
    }

    public void SetNextPipe(GameObject p)
    {
        nextPipe = p;
    }
}
