using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    private ArrayList pipes = new ArrayList();
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
        ballParent = new GameObject();
        ballParent.name = "VisualizationBallParent";
        Invoke("CollectStartingPipes", 0.2f);
        Invoke("InitializeSpeed", 2.5f);
    }

    private void Update()
    {
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

    private void InitializeSpeed()
    {
        speed = startingSpeed;
    }

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

    private void SpawnVisualizationBalls()
    {
        foreach (Object pipeObject in pipes)
        {
            GameObject pipe = (GameObject)pipeObject;

            GameObject ballInstance = Instantiate(visualizationBall, ballParent.transform);

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

    public void ToggleVisibility()
    {
        visible = !visible;
    }
}
