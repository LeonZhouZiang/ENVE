using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private bool canMove = false;
    [SerializeField] private float movementSpeed = 10;

    private void Update()
    {
        if (canMove)
        {
            Vector3 input = new Vector3(Input.GetAxis("MoveX"), Input.GetAxis("MoveY"), Input.GetAxis("MoveZ"));
            if (input.magnitude > 1)
            {
                input.Normalize();
            }
            transform.position += (input * movementSpeed * Time.deltaTime);
            Debug.Log(input);
        }
    }
}
