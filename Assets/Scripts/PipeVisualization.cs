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

        // If first pipe in section, the pipe is a starting pipe
        if (pipeNumber == 1)
        {
            startingPipe = true;
        }

        // Searches for what the next pipe should be. Returns that pipe if it exists, null if it doesn't
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

    // If pipe is a starting pipe, this determines the path from the starting pipe to the last pipe it should reach
    private void CollectPath()
    {
        if (!startingPipe)
        {
            return;
        }
        path = new ArrayList();
        // Adds the starting point to the list
        // The point is converted from local transform to global point
        path.Add(transform.TransformDirection(l.GetPosition(0) * transform.localScale.y) + transform.position);
        LineRenderer currentPipe = l;
        int previousPipeSection = section;
        // Runs until a pipe is found that has no next pipe
        while (true)
        {
            // Adds every position of the current pipe except for the first one, which is already accounted for
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
                    // Continues out one end of the t-pipe
                    if (t.IsReversed())
                    {
                        path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(2) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                        path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(0) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                    }
                    else
                    {
                        // Continues out the single path
                        if (((int.Parse(currentPipe.gameObject.name) / 100) % 100) - 1 == previousPipeSection)
                        {
                            path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(2) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                            path.Add(currentPipe.transform.TransformDirection(currentPipe.GetPosition(3) * currentPipe.transform.localScale.y) + currentPipe.transform.position);
                        }
                        // Ends path
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
