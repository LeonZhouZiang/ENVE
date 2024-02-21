using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bouyancy = 10, drag = .95f, waterHeight = 1.5f;
    [SerializeField] private Vector2 flowForce = Vector2.zero;
    private bool floatingPhysicsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (floatingPhysicsEnabled)
        {
            rb.velocity *= drag;
            if (transform.position.y < waterHeight)
            {
                rb.AddForce(new Vector3(flowForce.x, bouyancy, flowForce.y));
            }
        }
    }

    public void DisableFloatingPhysics()
    {
        floatingPhysicsEnabled = false;
    }

    public void EnableFloatingPhysics()
    {
        floatingPhysicsEnabled = true;
    }
}
