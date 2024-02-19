using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bouyancy = 10, drag = .95f;
    [SerializeField] private Vector2 flowForce = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity *= drag;
        if (transform.position.y < 1.5f)
        {
            rb.AddForce(new Vector3(flowForce.x, bouyancy, flowForce.y));
        }
    }
}
