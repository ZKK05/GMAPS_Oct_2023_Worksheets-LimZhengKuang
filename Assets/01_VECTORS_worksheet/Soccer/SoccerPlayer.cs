using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class SoccerPlayer : MonoBehaviour
{
    public bool IsCaptain = false;
    public SoccerPlayer[] OtherPlayers;
    public float rotationSpeed = 1f;

    float angle = 0f;

    private void Start()
    {
        //OtherPlayers = FindObjectsOfType<SoccerPlayer>();
        //SoccerPlayer[] temp = new SoccerPlayer[OtherPlayers.Length -1];
        //int i = 0;

        //foreach (SoccerPlayer player in OtherPlayers)
        //{
        //    if(player != this)
        //    {
        //        temp[i] = player;
        //        i++;
        //    }
            
        //}
        //OtherPlayers = temp;
        

        OtherPlayers = FindObjectsOfType<SoccerPlayer>().Where(t => t != this).ToArray();
        print("Number of Players is " + OtherPlayers.Length);

        if (IsCaptain)
        {
            FindMinimum();
        }
    }

    void FindMinimum()
    {
        float mini = 21; 
        for (int i = 0; i<10; i++)
        {
            float height = Random.Range(5f, 20f);
            Debug.Log(height);

            if(height < mini) //the random numbers will be compared to the mini value where it will keep checking and
                              // the mini value will be keep decreasing each time after it is compared as long as height is lower than mini
            {
                mini = height * 1; 
            }
        }
        print("The minimum height is " + mini);
    }

    float Magnitude(Vector3 vector)
    {
        return (float)Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z));
    }

    Vector3 Normalise(Vector3 vector)
    {
        float mag = Magnitude(vector);
        vector.x /= mag;
        vector.y /= mag;
        vector.z /= mag;
        return vector;
    }

    float Dot(Vector3 vectorA, Vector3 vectorB)
    {
        return (vectorA.x *  vectorB.x + vectorA.y * vectorB.y + vectorA.z * vectorB.z);
    }

    SoccerPlayer FindClosestPlayerDot()
    {
        SoccerPlayer closest = null;
        float minAngle = 180f;

        for (int i = 0; i< OtherPlayers.Length; i++)
        {
            Debug.Log(OtherPlayers[i]);
            Vector3 B = transform.forward;
            Normalise(B);
            Vector3 toPlayer = OtherPlayers[i].transform.position - transform.position ;
            Vector3 toPlayerNorm = Normalise(toPlayer);

            float dot = Dot(toPlayerNorm, B);
            float angle = (float)Mathf.Acos(dot);
            angle = angle * Mathf.Rad2Deg;

            if (angle < minAngle)
            {
                minAngle = angle * 1;
                closest = OtherPlayers[i];
            } 
        }
        //Debug.Log(closest);
        //Debug.Log(angle);
        return closest;
     }

    void DrawVectors()
    {
        foreach (SoccerPlayer other in OtherPlayers)
        {
            Debug.DrawRay(transform.position, other.transform.position - transform.position , Color.black);
            
        }
    }

    void Update()
    {
        DebugExtension.DebugArrow(transform.position, transform.forward, Color.red);

        if (IsCaptain)
        {
            angle += Input.GetAxis("Horizontal") * rotationSpeed;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

            DrawVectors();

            SoccerPlayer targetPlayer = FindClosestPlayerDot();
            targetPlayer.GetComponent<Renderer>().material.color = Color.green;

            foreach (SoccerPlayer other in OtherPlayers.Where(t => t!= targetPlayer))
            {
                other.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}


