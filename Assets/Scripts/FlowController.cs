using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    private ArrayList pipes = new ArrayList();
    private ArrayList pipeGroups = new ArrayList();
    private bool visible = false;
    [SerializeField] private GameObject visualizationBall;
    private GameObject ballParent;
    [SerializeField] private float startingSpeed = 10;
    private float speed = 100;
    private float previousSpeed;

    // Start is called before the first frame update
    void Start()
    {
        previousSpeed = speed;

        // Organizational parent object
        ballParent = new GameObject();
        ballParent.name = "VisualizationBallParent";

        Invoke("CollectStartingPipes", 0.2f);
        Invoke("CollectPipeGroups", 0.2f);
        Invoke("InitializeSpeed", 2.5f);
        Invoke("PrintPipeGroups", 5f);
    }

    private void Update()
    {
        // Detects a speed change and updates the speed for all existing balls
        if (previousSpeed != speed)
        {
            previousSpeed = speed;
            GameObject[] balls = GameObject.FindGameObjectsWithTag("VisualizationBall");
            foreach (GameObject ball in balls)
            {
                ball.GetComponent<VisualizationBall>().SetSpeed(speed);
            }
        }
    }

    // Balls move at super speed at the beginning to propogate the pipes
    // This method turns off super-speed
    private void InitializeSpeed()
    {
        speed = startingSpeed;
    }

    private void CollectPipeGroups()
    {
        GameObject[] allPipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject p in allPipes)
        {
            int idNumber = int.Parse(p.name);
            int group = idNumber / 10000;

            if (pipeGroups.Count < group)
            {
                for (int i = pipeGroups.Count; i < group; i++)
                {
                    pipeGroups.Add(new ArrayList());
                }
            }

            ArrayList pipeGroup = (ArrayList)pipeGroups[group - 1];
            pipeGroup.Add(p);
        }
    }

    private void PrintPipeGroups()
    {
        for (int i = 0; i < pipeGroups.Count; i++)
        {
            ArrayList pipeGroup = (ArrayList)pipeGroups[i];
            for (int j = 0; j < pipeGroup.Count; j++)
            {
                GameObject pipe = (GameObject)pipeGroup[j];
                Debug.Log(pipe.name);
            }
        }
    }

    public ArrayList GetPipeGroup(int group)
    {
        group--;
        ArrayList pipeGroup = (ArrayList)pipeGroups[group];
        return pipeGroup;
    }

    // Looks at all the pipes in the scene and creates an array list of only the starting pipes in a section
    private void CollectStartingPipes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentChild = transform.GetChild(i).gameObject;

            for (int j = 0; j < currentChild.transform.childCount; j++)
            {
                GameObject currentPipe = currentChild.transform.GetChild(j).gameObject;

                PipeVisualization p;
                currentPipe.TryGetComponent<PipeVisualization>(out p);
                if (p)
                {
                    if (p.IsStartingPipe())
                    {
                        pipes.Add(currentPipe);
                    }
                    continue;
                }

                TPipeVisualization t;
                currentPipe.TryGetComponent<TPipeVisualization>(out t);
                if (t)
                {
                    if (t.IsReversed())
                    {
                        pipes.Add(currentPipe);
                    }
                    continue;
                }
            }
        }
        StartCoroutine(SpawnTimer());
    }

    // Spawns balls at a rate proportional to the ball speed to maintain equal spacing between balls
    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            SpawnVisualizationBalls();
            for (float f = 0; f < 10/speed; f += Time.deltaTime)
            {
                yield return null;
            }
        }
    }

    // Spawns one ball for every starting pipe
    private void SpawnVisualizationBalls()
    {
        foreach (Object pipeObject in pipes)
        {
            GameObject pipe = (GameObject)pipeObject;
            int idNumber = int.Parse(pipe.name);
            int group = idNumber / 10000;

            GameObject ballInstance = Instantiate(visualizationBall, ballParent.transform);
            ballInstance.GetComponent<VisualizationBall>().SetGroupNumber(group);

            if (visible)
            {
                ballInstance.GetComponent<VisualizationBall>().ToggleVisibility();
            }
            ballInstance.GetComponent<VisualizationBall>().SetSpeed(speed);

            PipeVisualization p;
            pipe.TryGetComponent<PipeVisualization>(out p);
            if (p)
            {
                ballInstance.GetComponent<VisualizationBall>().SetPath(p.GetPath());
                continue;
            }

            TPipeVisualization t;
            pipe.TryGetComponent<TPipeVisualization>(out t);
            if (t)
            {
                ballInstance.GetComponent<VisualizationBall>().SetPath(t.GetPath());
                continue;
            }
        }
    }

    // Determines the initial visibility state for newly spawned balls
    public void ToggleVisibility()
    {
        visible = !visible;
    }
}
