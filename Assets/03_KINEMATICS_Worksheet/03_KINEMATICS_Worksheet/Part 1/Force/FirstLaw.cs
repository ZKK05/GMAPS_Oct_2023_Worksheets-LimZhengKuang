using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaw : MonoBehaviour
{
    public Vector3 force; //able to apply force by setting x,y,z of the ball in the inspecter
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //get ridigbody component of ball
        rb.AddForce(force, ForceMode.Impulse); //apply force on ball for a short moment in time
     }

    void FixedUpdate()
    {
        Debug.Log(transform.position);
    }
}

