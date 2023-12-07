using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static float FindDistance(HVector2D p1, HVector2D p2)
    {
        return (p2 - p1).Magnitude(); //find distance between two points and is calculated by finding the magnitude of the subtraction of the two vectors
    }
}

