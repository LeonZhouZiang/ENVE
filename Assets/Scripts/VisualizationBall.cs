using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationBall : MonoBehaviour
{
    [SerializeField] private Material visibleMaterial, invisibleMaterial;
    private ArrayList path;
    private float speed = 10;
    private bool visible = false;
    [SerializeField] private MeshRenderer mr;
    private int pipeGroupNumber;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TraversePath());
    }

    public void SetPath(ArrayList p)
    {
        path = p;
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }

    public bool GetVisiblility()
    {
        return visible;
    }

    public int GetGroupNumber()
    {
        return pipeGroupNumber;
    }

    public void ToggleVisibility()
    {
        if (visible)
        {
            mr.material = invisibleMaterial;
        }
        else
        {
            mr.material = visibleMaterial;
        }
        visible = !visible;
    }

    private IEnumerator TraversePath()
    {
        float leftover = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 pos1 = (Vector3)path[i];
            Vector3 pos2 = (Vector3)path[i + 1];
            float distanceBetweenPositions = Mathf.Abs(Vector3.Distance(pos1, pos2));
            float distanceTraveled = leftover;
            for (; distanceTraveled < distanceBetweenPositions; distanceTraveled += Time.deltaTime * speed)
            {
                transform.position = Vector3.Lerp(pos1, pos2, distanceTraveled / distanceBetweenPositions);
                yield return null;
            }
            leftover = distanceTraveled - distanceBetweenPositions;
        }
        Destroy(gameObject);
    }

    public void SetGroupNumber(int val)
    {
        pipeGroupNumber = val;
    }

    public void SetVisibleMaterial(Material m)
    {
        visibleMaterial = m;
    }
}
