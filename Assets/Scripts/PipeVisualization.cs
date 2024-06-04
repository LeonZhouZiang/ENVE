using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeVisualization : MonoBehaviour
{
    [SerializeField] private GameObject flowSphere;
    private GameObject fSphere;
    private LineRenderer l;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<LineRenderer>();
        fSphere = Instantiate(flowSphere);
        StartCoroutine(MoveFlowSphere());
    }

    private IEnumerator MoveFlowSphere()
    {
        while (true)
        {
            for (int i = 0; i < l.positionCount - 1; i++)
            {
                for (float f = 0; f < 1; f += Time.deltaTime)
                {
                    fSphere.transform.position = Vector3.Lerp(transform.TransformDirection(l.GetPosition(i) * 100) + transform.position, transform.TransformDirection(l.GetPosition(i + 1) * 100) + transform.position, f);
                    yield return null;
                }
            }
        }
    }
}
