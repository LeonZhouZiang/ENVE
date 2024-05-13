using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private bool physicsEnabled = true;

    public bool PhysicsAreEnabled()
    {
        return physicsEnabled;
    }

    public void EnablePhysics()
    {
        physicsEnabled = true;
    }

    public void DisablePhysics()
    {
        physicsEnabled = false;
    }
}
