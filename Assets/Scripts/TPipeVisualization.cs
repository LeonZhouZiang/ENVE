using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPipeVisualization : MonoBehaviour
{
    [SerializeField] private GameObject flowSphere;
    // A normal tpipe combines two pipes into one
    // A reverse tpipe splits one pipe into two
    [SerializeField] private bool reverse;
    private GameObject fSphere1, fSphere2, nextPipe, secondNextPipe;
    private LineRenderer l;
    private float moveSpeed = 10;
    private int group, section, pipeNumber;
    private ArrayList path;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<LineRenderer>();
        Initialize();
        if (secondNextPipe)
        {
            Debug.Log(gameObject.name + " " + nextPipe.name + " " + secondNextPipe.name);
        }
        else
        {
            Debug.Log(gameObject.name + " " + nextPipe.name);
        }

        if (reverse)
        {
            //Invoke("CollectPath", 0.1f);
        }

        /*
        fSphere1 = Instantiate(flowSphere);
        fSphere1.SetActive(false);
        fSphere2 = Instantiate(flowSphere);
        fSphere2.SetActive(false);

        if (!reverse)
        {
            StartCoroutine(MoveFlowSphere1());
            StartCoroutine(MoveFlowSphere2());
        }
        else
        {
            StartCoroutine(MoveFlowSphere1Reverse());
            StartCoroutine(MoveFlowSphere2Reverse());
        }
        */
    }

    public bool IsReversed()
    {
        return reverse;
    }

    public GameObject GetNextPipe()
    {
        return nextPipe;
    }

    public GameObject GetSecondNextPipe()
    {
        return secondNextPipe;
    }

    private void Initialize()
    {
        int idNumber = int.Parse(gameObject.name);
        pipeNumber = idNumber % 100;
        section = (idNumber / 100) % 100;
        group = idNumber / 10000;

        if (!reverse)
        {
            // Find next pipe
            string potentialNextPipe = "";
            if (group < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += group;
            if (section + 1 < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += (section + 1);
            potentialNextPipe += 0;
            potentialNextPipe += 1;
            nextPipe = GameObject.Find(potentialNextPipe);
            PipeVisualization p;
            nextPipe.TryGetComponent<PipeVisualization>(out p);
            if (p)
            {
                p.TurnOffStartingPipe();
            }

            // Find first previous pipe
            string potentialPreviousPipe = "";
            if (group < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += group;
            if (section - 1 < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += (section - 1);
            potentialPreviousPipe += 0;
            potentialPreviousPipe += 1;

            GameObject startingPipeOfPreviousSection = GameObject.Find(potentialPreviousPipe);
            GameObject previousPipe;

            while (true)
            {
                previousPipe = startingPipeOfPreviousSection;
                startingPipeOfPreviousSection = startingPipeOfPreviousSection.GetComponent<PipeVisualization>().GetNextPipe();
                if (!startingPipeOfPreviousSection)
                {
                    break;
                }
            }

            previousPipe.GetComponent<PipeVisualization>().SetNextPipe(gameObject);

            // Find second previous pipe
            potentialPreviousPipe = "";
            if (group < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += group;
            if (section - 2 < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += (section - 2);
            potentialPreviousPipe += 0;
            potentialPreviousPipe += 1;

            startingPipeOfPreviousSection = GameObject.Find(potentialPreviousPipe);

            while (true)
            {
                previousPipe = startingPipeOfPreviousSection;
                startingPipeOfPreviousSection = startingPipeOfPreviousSection.GetComponent<PipeVisualization>().GetNextPipe();
                if (!startingPipeOfPreviousSection)
                {
                    break;
                }
            }

            previousPipe.GetComponent<PipeVisualization>().SetNextPipe(gameObject);
        }
        else
        {
            // Find first next pipe
            string potentialNextPipe = "";
            if (group < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += group;
            if (section + 1 < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += (section + 1);
            potentialNextPipe += 0;
            potentialNextPipe += 1;
            nextPipe = GameObject.Find(potentialNextPipe);
            PipeVisualization p;
            nextPipe.TryGetComponent<PipeVisualization>(out p);
            if (p)
            {
                p.TurnOffStartingPipe();
            }

            // Find second next pipe
            potentialNextPipe = "";
            if (group < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += group;
            if (section + 2 < 10)
            {
                potentialNextPipe += 0;
            }
            potentialNextPipe += (section + 2);
            potentialNextPipe += 0;
            potentialNextPipe += 1;
            secondNextPipe = GameObject.Find(potentialNextPipe);
            secondNextPipe.TryGetComponent<PipeVisualization>(out p);
            if (p)
            {
                p.TurnOffStartingPipe();
            }

            // Find previous pipe
            string potentialPreviousPipe = "";
            if (group < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += group;
            if (section - 1 < 10)
            {
                potentialPreviousPipe += 0;
            }
            potentialPreviousPipe += (section - 1);
            potentialPreviousPipe += 0;
            potentialPreviousPipe += 1;

            GameObject startingPipeOfPreviousSection = GameObject.Find(potentialPreviousPipe);
            GameObject previousPipe;

            while (true)
            {
                previousPipe = startingPipeOfPreviousSection;
                startingPipeOfPreviousSection.TryGetComponent<PipeVisualization>(out p);
                if (p)
                {
                    startingPipeOfPreviousSection = p.GetNextPipe();
                }
                else
                {
                    startingPipeOfPreviousSection = startingPipeOfPreviousSection.GetComponent<TPipeVisualization>().GetNextPipe();
                    break;
                }

                if (!startingPipeOfPreviousSection)
                {
                    previousPipe.GetComponent<PipeVisualization>().SetNextPipe(gameObject);
                    break;
                }
            }
        }
    }

    private void CollectPath()
    {
        path = new ArrayList();
        path.Add(transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position);
        path.Add(transform.TransformDirection(l.GetPosition(1) * transform.localScale.y) + transform.position);
        LineRenderer currentPipe = secondNextPipe.GetComponent<LineRenderer>();
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
                    currentPipe = t.GetNextPipe().GetComponent<LineRenderer>();
                    previousPipeSection = (int.Parse(currentPipe.gameObject.name) / 100) % 100;
                }
            }
            previousPipeSection = (int.Parse(currentPipe.gameObject.name) / 100) % 100;
        }
    }

    /*
    private IEnumerator MoveFlowSphere1()
    {
        while (true)
        {
            float leftover = 0;
            Vector3 pos1 = transform.TransformDirection(l.GetPosition(0) * transform.localScale.y) + transform.position;
            Vector3 pos2 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            float distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere1.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled/distanceBetweenPositions);
                yield return null;
            }
            leftover = distanceTraveled - distanceBetweenPositions;

            pos1 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            pos2 = transform.TransformDirection(l.GetPosition(3) * transform.localScale.y) + transform.position;
            distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere1.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
        }
    }

    private IEnumerator MoveFlowSphere2()
    {
        while (true)
        {
            float leftover = 0;
            Vector3 pos1 = transform.TransformDirection(l.GetPosition(1) * transform.localScale.y) + transform.position;
            Vector3 pos2 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            float distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere2.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
            leftover = distanceTraveled - distanceBetweenPositions;

            pos1 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            pos2 = transform.TransformDirection(l.GetPosition(3) * transform.localScale.y) + transform.position;
            distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere2.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
        }
    }

    private IEnumerator MoveFlowSphere1Reverse()
    {
        while (true)
        {
            float leftover = 0;
            Vector3 pos1 = transform.TransformDirection(l.GetPosition(3) * transform.localScale.y) + transform.position;
            Vector3 pos2 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            float distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere1.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
            leftover = distanceTraveled - distanceBetweenPositions;

            pos1 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            pos2 = transform.TransformDirection(l.GetPosition(0) * transform.localScale.y) + transform.position;
            distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere1.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
        }
    }

    private IEnumerator MoveFlowSphere2Reverse()
    {
        while (true)
        {
            float leftover = 0;
            Vector3 pos1 = transform.TransformDirection(l.GetPosition(3) * transform.localScale.y) + transform.position;
            Vector3 pos2 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            float distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere2.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
            leftover = distanceTraveled - distanceBetweenPositions;

            pos1 = transform.TransformDirection(l.GetPosition(2) * transform.localScale.y) + transform.position;
            pos2 = transform.TransformDirection(l.GetPosition(1) * transform.localScale.y) + transform.position;
            distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * moveSpeed)
            {
                fSphere2.transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
        }
    }

    public void ToggleVisuals()
    {
        fSphere1.SetActive(!fSphere1.activeSelf);
        fSphere2.SetActive(!fSphere2.activeSelf);
    }
    */
}
