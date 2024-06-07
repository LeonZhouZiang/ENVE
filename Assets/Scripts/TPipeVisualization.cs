using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPipeVisualization : MonoBehaviour
{
    [SerializeField] private GameObject nextPipe, secondNextPipe;
    [SerializeField] private GameObject flowSphere;
    [SerializeField] private bool reverse;
    private GameObject fSphere1, fSphere2;
    private LineRenderer l;
    private float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
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
