﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 thrustForce = new Vector3(0f,0f,45f);
    public Vector3 rotationTorque = new Vector3(0f,8f,0f);


    public bool controlsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        controlsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsEnabled)
        {
            //moving forward
            if (Input.GetKey("w"))
            {
                rb.AddRelativeForce(thrustForce);
            }

            // moving backward
            if (Input.GetKey("s"))
            {
                rb.AddRelativeForce(-thrustForce);
            }


            //turning left
            if (Input.GetKey("a"))
            {
                rb.AddRelativeTorque(-rotationTorque);
            }

            //turning right
            if (Input.GetKey("d"))
            {
                rb.AddRelativeTorque(rotationTorque);
            }
        }
    }
}
