using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2D : MonoBehaviour
{
    public List<Ball2D> balls; //get list of balls in scene

    private void Start()
    {

    }

    bool CheckBallCollision(Ball2D toCheck)//check for if ball collides with another
    {
        for (int i = 0; i < balls.Count; i++) //for every ball in list,
        {
            Ball2D ball = balls[i]; //is i not 1

            if (ball.IsCollidingWith(toCheck) && toCheck != ball) //if ball(cue) is colliding with another ball and ball is not colliding with itself, 
            {
                return true; //return true, collision is true
            }
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (CheckBallCollision(balls[0])) //check collisions of cue ball, if there is,
        {
            Debug.Log("COLLISION!");//return "COLLISION" message
        }
    }
}
