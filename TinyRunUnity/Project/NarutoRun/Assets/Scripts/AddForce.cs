using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    private Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RB.AddForce(new Vector3(0, 0, -15));
    }
}
