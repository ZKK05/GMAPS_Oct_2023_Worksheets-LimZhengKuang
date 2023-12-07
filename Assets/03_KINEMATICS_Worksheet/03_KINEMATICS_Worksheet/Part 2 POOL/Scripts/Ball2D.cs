using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2D : MonoBehaviour
{
    public HVector2D Position = new HVector2D(0, 0); //declaring vector2D position & velocity of ball
    public HVector2D Velocity = new HVector2D(0, 0);
    

    [HideInInspector]
    public float Radius;

    private void Start()
    {
        Position.x = transform.position.x; // set the initial position of the ball based on the gameobject's position
        Position.y = transform.position.y;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite; //get sprite renderer from gameobject
        Vector2 sprite_size = sprite.rect.size; 
        Vector2 local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        Radius = local_sprite_size.x / 2f; //set radius to sprite's size
        HVector2D a = new HVector2D(8f, 5f); //set variable a's vector
        HVector2D b = new HVector2D(1f, 3f); //set variable b's vector
        float distance = Util.FindDistance(a, b); //demo the util function using a, b as an example. Find the distance between two vectors
    }

    public bool IsCollidingWith(float x, float y) //if ball is colliding with point (x, y)
    {
        float distance = Util.FindDistance(Position, new HVector2D(x, y)); //find distance between the ball's position and new point (x, y)
        return distance <= Radius; //return distance if distance is less than or equals to radius of ball
    }

    public bool IsCollidingWith(Ball2D other) //if ball is colliding with another ball
    {
        float distance = Util.FindDistance(Position, other.Position); //find distance between the two balls
        return distance <= Radius + other.Radius; //check if the calculated distance is less than or equal to the sum of the radii, if collision is true, indicate collision
    }

    public void FixedUpdate()
    {
        UpdateBall2DPhysics(Time.deltaTime); //updates physics of ball based on seconds from last frame to current frame
    }

    private void UpdateBall2DPhysics(float deltaTime)
    {
        float displacementX = Velocity.x * deltaTime; //set displacement values x & y based on velocity which is changed in the poolcue script
        float displacementY = Velocity.y * deltaTime; //velocity will be more if the line drawn is longer

        Position.x += -displacementX; //update ball position based on displacement values
        Position.y += -displacementY;

        transform.position = new Vector2(Position.x, Position.y); //update ball's position based on new x & y values
    }
}

