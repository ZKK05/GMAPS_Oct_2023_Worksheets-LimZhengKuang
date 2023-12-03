using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToHeight : MonoBehaviour
{
    public float Height = 1f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Jump()
    {
        //v * v = u * u + 2 as
        //u*u = v * v - 2 as
        //u = sqrt(v * v - 2 as)
        //v = 0, u = ?, a = physics.gravity, s = height
        //float v = 0;
        //float a = Physics.gravity.y;
        //float s = Height;

        //float u = Mathf.Sqrt(v*v - (2*a*s));
        //rb.velocity = new Vector3();

        float jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * Height);
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Jump();
        }
    }
}

