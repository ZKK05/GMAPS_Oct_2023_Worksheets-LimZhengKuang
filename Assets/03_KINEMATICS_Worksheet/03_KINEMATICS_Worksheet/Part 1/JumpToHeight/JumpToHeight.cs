using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToHeight : MonoBehaviour
{
    public float Height = 1f; //jump height value
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody component from game object
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

        float u = Mathf.Sqrt(-2 * Physics2D.gravity.y * Height); //getting the velocity required for desired jump height to escape the gravity 
                                                                 //that is why -2 * gravity since it cancels out the gravity by making it positive

        rb.velocity = new Vector3(0, u, 0);

        //float jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * Height);
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //space bar has been pressed down, call jump function
        {
            
            Jump();
        }
    }
}

