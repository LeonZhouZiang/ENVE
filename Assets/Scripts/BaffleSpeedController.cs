using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaffleSpeedController : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("SpeedMultiplier", speedMultiplier);
    }
}
