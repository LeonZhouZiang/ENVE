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

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        l = GetComponent<LineRenderer>();
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
                startingPipeOfPreviousSection = startingPipeOfPreviousSection.GetComponent<PipeVisualization>().GetNextPipe();
                if (!startingPipeOfPreviousSection)
                {
                    break;
                }
            }

            previousPipe.GetComponent<PipeVisualization>().SetNextPipe(gameObject);
        }
    }

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
}
