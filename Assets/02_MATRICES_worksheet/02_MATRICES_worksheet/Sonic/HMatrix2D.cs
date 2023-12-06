using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class HMatrix2D
{
    public float[,] Entries { get; set; } = new float[3, 3];

    public HMatrix2D()
    {
        // your code here
    }

    public HMatrix2D(float[,] multiArray) //constructor that accepts 2d array paramteres
    {
        for (int x = 0; x < 3; x++)// for every x and every y, take the respective values to the matrix entries
        {
            for (int y = 0; y < 3; y++) 
            {
                Entries[x, y] = multiArray[x, y];
            }
        }
    }

    public HMatrix2D(float m00, float m01, float m02, //constructor for HMatrix2D that directly sets its values based on provided elements
                                                      // initialized transformation matrices
             float m10, float m11, float m12,
             float m20, float m21, float m22)
    {
        // Set the matrix entries directly using the provided parameters
        // Each parameter corresponds to a specific position in the 3x3 matrix

        //First row, followed by second and third rows
        Entries[0,0] = m00; //row 0, column 0
        Entries[0,1] = m01; 
        Entries[0,2] = m02;


        Entries[1,0] = m10;
        Entries[1,1] = m11;
        Entries[1,2] = m12;


        Entries[2,0] = m20;
        Entries[2,1] = m21;
        Entries[2,2] = m22;

       
    }

    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right) //addition constructor
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++) //for every x and y values, add it to the left matrix entry and the right matrix entry respectively
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] + right.Entries[x, y]; //get the final addition product
            }
        }
        return result;
               
    }

    public static HMatrix2D operator -(HMatrix2D left, HMatrix2D right) //subtraction constructor
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++) //for every x and y values, add it to the left matrix entry and right matrix entry respectively
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] - right.Entries[x, y]; //final subtraction product
            }
        }
        return result;
    }

    public static HMatrix2D operator *(HMatrix2D left, float scalar) //multipication with scalar constructor
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++) //for every x and y values, add it to the matrix entry
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] * scalar; //matrix multiply by the scalar product equals to the result
            }
        }
        return result;
    }

    // Note that the second argument is a HVector2D object
    //
    public static HVector2D operator *(HMatrix2D left, HVector2D right) //multipication of a matrix and a vector
    {
        return new HVector2D(
            left.Entries[0,0] * right.x + left.Entries[0,1] * right.y + left.Entries[0,2] * right.h, //resultant vector is calculated by multiplying the 
            left.Entries[1,0] * right.x + left.Entries[1,1] * right.y + left.Entries[1,2] * right.h);//corresponding elements of the first row of the matrix (left.Entries[0, 0], left.Entries[0, 1], left.Entries[0, 2])
    }                                                                                                //with the components of the vector (right.x, right.y, right.h)

    // Note that the second argument is a HMatrix2D object
    //
    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right) //multiplying a 3x3 matrix by a 3x3 matrix, row times column respectively
    {
        return new HMatrix2D(
            // Calculate the new entry at position (0, 0) in the result matrix, followed by (0, 1), (0,2)
            left.Entries[0, 0] * right.Entries[0, 0] + left.Entries[0, 1] * right.Entries[1, 0] + left.Entries[0, 2] * right.Entries[2, 0], //right entries change, left same
            left.Entries[0, 0] * right.Entries[0, 1] + left.Entries[0, 1] * right.Entries[1, 1] + left.Entries[0, 2] * right.Entries[2, 1], //m00, m01, m02
            left.Entries[0, 0] * right.Entries[0, 2] + left.Entries[0, 1] * right.Entries[1, 2] + left.Entries[0, 2] * right.Entries[2, 2],

            //calculate the same way but instead at position (1, 0), followed by (1, 1), (1, 2)
            left.Entries[1, 0] * right.Entries[0, 0] + left.Entries[1, 1] * right.Entries[1, 0] + left.Entries[1, 2] * right.Entries[2, 0], //m10, m11,m12
            left.Entries[1, 0] * right.Entries[0, 1] + left.Entries[1, 1] * right.Entries[1, 1] + left.Entries[1, 2] * right.Entries[2, 1],
            left.Entries[1, 0] * right.Entries[0, 2] + left.Entries[1, 1] * right.Entries[1, 2] + left.Entries[1, 2] * right.Entries[2, 2],

            //calculate same way at positions (2, 0), (2, 1) and finally (2, 2)
            left.Entries[2, 0] * right.Entries[0, 0] + left.Entries[2, 1] * right.Entries[1, 0] + left.Entries[2, 2] * right.Entries[2, 0], //m20, m21, m22
            left.Entries[2, 0] * right.Entries[0, 1] + left.Entries[2, 1] * right.Entries[1, 1] + left.Entries[2, 2] * right.Entries[2, 1],
            left.Entries[2, 0] * right.Entries[0, 2] + left.Entries[2, 1] * right.Entries[1, 2] + left.Entries[2, 2] * right.Entries[2, 2]
            );
    }
        // and so on for another 7 entries
        //);
        //}

    public static bool operator == (HMatrix2D left, HMatrix2D right)
    {
        for (int x = 0; x < 3; x++) //for every x value and y value of the two matrix
        {
            for (int y = 0; y < 3; y++)
            {
                if (left.Entries[x, y] != right.Entries[x, y]) //compare each respective X values & y values  
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator != (HMatrix2D left, HMatrix2D right)
    {
        for (int x = 0; x < 3; x++) //for every x value and y value of the two matrix
        {
            for (int y = 0; y < 3; y++)
            {
                if (left.Entries[x, y] == right.Entries[x, y]) //compare each respective X values & y values and check whether it is the same
                {
                    return false; //if all values are corresponding, return false
                }
            }
        }
        return true; //if values are different, return true
    }

    //public override bool Equals(object obj)
    //{
    // your code here
    //}

    //public override int GetHashCode()
    //{
    // your code here
    //}

    //public HMatrix2D transpose()
    //{
    //return // your code here
    //}

    //public float getDeterminant()
    //{
    //return // your code here
    //}

    public void setIdentity()
    {
        for (int x = 0 ; x < 3; x++)
        {
            for (int y = 0 ; y < 3; y++) //for every x and y value,
            {
                Entries[x, y] = x == y ? 1 : 0; //Set x and y value to 1 if x equals to y, else, set value to 0
                //Creates the identity matrix where all diagonal elements are 1 and non-diagonal elements are0
            }
        }
    }

    public void setTranslationMat(float transX, float transY)
    {
        setIdentity(); //call identity matrix
        Entries[0, 2] = transX; //set the trans X to position (0, 2)
        Entries[1, 2] = transY; //set the trans Y to position (1, 2) such that the identity matrix has values transX at (0,2) for x-axis translation and transY at (1, 2) for y-axis translation
    }

    public void setRotationMat(float rotDeg)
    {
        setIdentity() ; //call identity matrix
        float rad = rotDeg * Mathf.Deg2Rad; //convert degrees into radians
        Entries[0,0] = Mathf.Cos(rad); //based on the math formula, set the values Cos(rad) to position (0,0) and (1, 1)
        Entries[0,1] = (Mathf.Sin(rad) * -1);// set value Sin(Rad) to (1, 0) and negative Sin(Rad) to (0, 1)
        Entries[1, 0] = Mathf.Sin(rad);
        Entries[1, 1] = Mathf.Cos(rad);

    }

    public void setScalingMat(float scaleX, float scaleY)
    {
        // your code here
    }

    public void Print()
    {
        string result = "";
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                result += Entries[r, c] + "  ";
            }
            result += "\n";
        }
        Debug.Log(result);
    }
}
