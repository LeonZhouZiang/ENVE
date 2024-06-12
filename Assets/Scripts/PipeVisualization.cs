using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeVisualization : MonoBehaviour
{
    private GameObject nextPipe = null;
    private bool startingPipe;
    private LineRenderer l;
    private int group, section, pipeNumber;
    private ArrayList path;

    private void Awake()
    {
        nextPipe = Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<LineRenderer>();
        Invoke("CollectPath", 0.1f);
        /*
        fSphere = Instantiate(flowSphere);
        fSphere.SetActive(false);
        StartCoroutine(MoveFlowSphere());
        */
    }

    public bool IsStartingPipe()
    {
        return startingPipe;
    }

    public void TurnOffStartingPipe()
    {
        startingPipe = false;
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

    private void CollectPath()
    {
        if (!startingPipe)
        {
            return;
        }
        path = new ArrayList();
        path.Add(transform.TransformDirection(l.GetPosition(0) * transform.localScale.y) + transform.position);
        LineRenderer currentPipe = l;
        int previousPipeSection = section;
        while (true)
        {
            for (int i = 1; i < currentPipe.positionCount; i++)
            {
                path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(i) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
            }
            GameObject nextPipe = currentPipe.gameObject.GetComponent<PipeVisualization>().GetNextPipe();

            // Stops if there are no more pipes after this one
            if (nextPipe == null)
            {
                return;
            }

            currentPipe = nextPipe.GetComponent<LineRenderer>();

            // Handles t-pipes
            while (true)
            {
                // Checks for t-pipes
                TPipeVisualization t;
                if (!currentPipe.TryGetComponent<TPipeVisualization>(out t))
                {
                    break;
                }
                // Is a t-pipe
                else
                {
                    if (t.IsReversed())
                    {
                        path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(2) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                        path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(0) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                    }
                    else
                    {
                        // Continue path
                        if (((int.Parse(currentPipe.gameObject.name) / 100) % 100) - 1 == previousPipeSection)
                        {
                            path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(2) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                            path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(3) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                        }
                        // End path
                        else
                        {
                            path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(2) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                            return;
                        }
                    }
                    nextPipe = t.GetNextPipe();
                    currentPipe = nextPipe.GetComponent<LineRenderer>();
                    previousPipeSection = (int.Parse(currentPipe.gameObject.name) / 100) % 100;
                }
            }
            previousPipeSection = (int.Parse(currentPipe.gameObject.name) / 100) % 100;
        }
    }

    public GameObject GetNextPipe()
    {
        return nextPipe;
    }

    public void SetNextPipe(GameObject p)
    {
        nextPipe = p;
    }

    public ArrayList GetPath()
    {
        return path;
    }
}
