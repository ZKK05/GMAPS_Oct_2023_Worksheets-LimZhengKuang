using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCue : MonoBehaviour
{
    public LineFactory lineFactory;
    public GameObject ballObject;

    private Line drawnLine;
    private Ball2D ball;

    
    private void Start()
    {
        ball = ballObject.GetComponent<Ball2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if player presses down mouse button
        {

            var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // start line pos equals to mouse position from screen space to game world space
            if (ball != null && ball.IsCollidingWith(startLinePos.x , startLinePos.y))//if ball exists, and collides with start line
            {
                drawnLine = lineFactory.GetLine(ballObject.transform.position, startLinePos, 5, Color.black); //call the GetLine function which accepts parameters
                drawnLine.EnableDrawing(true);                                                                //start of the line is the ball's origin position, end line is the mouse position, width of line is 5f, color is black
                //Debug.Log("button draw true");
            }
        }
        else if (Input.GetMouseButtonUp(0) && drawnLine != null) //if player lifts up mouse button
        {
            drawnLine.EnableDrawing(false); //drawing is false

            //update the velocity of the white ball.
            HVector2D v = new HVector2D(drawnLine.end - drawnLine.start); //calculate velocity & direction based on how far back the line has been drawn previously
            ball.Velocity = v; //set the velocity to the ball

            drawnLine = null; // End line drawing            
        }

        if (drawnLine != null) // If a line is being drawn, update its end position based on the mouse position
        {
            drawnLine.end = Camera.main.ScreenToWorldPoint(Input.mousePosition)/*your code here*/; // Update line end
            //Debug.Log("End");
        }
    }

    /// <summary>
    /// Get a list of active lines and deactivates them.
    /// </summary>
    public void Clear()
    {
        var activeLines = lineFactory.GetActive(); //deactivates all active lines from LineFactory

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}
