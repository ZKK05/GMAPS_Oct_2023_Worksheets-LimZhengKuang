using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Vector3 Velocity;

    void FixedUpdate()
    {
        float dt = Time.deltaTime; //get time

        float dx = Velocity.x *dt; //find the distance travelled on the X, Y & Z axis based on velcotiy * time
        float dy = Velocity.y *dt;
        float dz = Velocity.z *dt;

        transform.Translate(new Vector3(dx, dy, dz)); //updates the ball based on distance travelled on each axis
    }
}
