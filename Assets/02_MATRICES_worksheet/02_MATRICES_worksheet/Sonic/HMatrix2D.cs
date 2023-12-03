using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HMatrix2D
{
    public float[,] Entries { get; set; } = new float[3, 3];

    public HMatrix2D()
    {
        // your code here
    }

    public HMatrix2D(float[,] multiArray)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++) 
            {
                Entries[x, y] = multiArray[x, y];
            }
        }
    }

    public HMatrix2D(float m00, float m01, float m02,
             float m10, float m11, float m12,
             float m20, float m21, float m22)
    {
        Entries[0,0] = m00;
        Entries[0,1] = m01;
        Entries[0,2] = m02;


        Entries[1,0] = m10;
        Entries[1,1] = m11;
        Entries[1,2] = m12;


        Entries[2,0] = m20;
        Entries[2,1] = m21;
        Entries[2,2] = m22;

       
    }

    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] + right.Entries[x, y];
            }
        }
        return result;
               
    }

    public static HMatrix2D operator -(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] - right.Entries[x, y];
            }
        }
        return result;
    }

    public static HMatrix2D operator *(HMatrix2D left, float scalar)
    {
        HMatrix2D result = new HMatrix2D();
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                result.Entries[x, y] = left.Entries[x, y] * scalar;
            }
        }
        return result;
    }

    // Note that the second argument is a HVector2D object
    //
    public static HVector2D operator *(HMatrix2D left, HVector2D right)
    {
        return new HVector2D(
            left.Entries[0,0] * right.x + left.Entries[0,1] * right.y + left.Entries[0,2] * right.h,
            left.Entries[1,0] * right.x + left.Entries[1,1] * right.y + left.Entries[1,2] * right.h);
    }

    // Note that the second argument is a HMatrix2D object
    //
    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right)
    {
        return new HMatrix2D(
            left.Entries[0, 0] * right.Entries[0, 0] + left.Entries[0, 1] * right.Entries[1, 0] + left.Entries[0, 2] * right.Entries[2, 0], //right entries change, left same
            left.Entries[0, 0] * right.Entries[0, 1] + left.Entries[0, 1] * right.Entries[1, 1] + left.Entries[0, 2] * right.Entries[2, 1], //m00, m01, m02
            left.Entries[0, 0] * right.Entries[0, 2] + left.Entries[0, 1] * right.Entries[1, 2] + left.Entries[0, 2] * right.Entries[2, 2],

            left.Entries[1, 0] * right.Entries[0, 0] + left.Entries[1, 1] * right.Entries[1, 0] + left.Entries[1, 2] * right.Entries[2, 0], //m10, m11,m12
            left.Entries[1, 0] * right.Entries[0, 1] + left.Entries[1, 1] * right.Entries[1, 1] + left.Entries[1, 2] * right.Entries[2, 1],
            left.Entries[1, 0] * right.Entries[0, 2] + left.Entries[1, 1] * right.Entries[1, 2] + left.Entries[1, 2] * right.Entries[2, 2],

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
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (left.Entries[x, y] != right.Entries[y, x])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator != (HMatrix2D left, HMatrix2D right)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (left.Entries[x, y] == right.Entries[y, x])
                {
                    return false;
                }
            }
        }
        return true;
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
            for (int y = 0 ; y < 3; y++)

            //{
            //if(x == y)
            //{
            //Entries[x, y] = 1;
            //}
            //else
            //{
            //Entries[x, y] = 0;
            //}

            {
                Entries[x, y] = x == y ? 1 : 0;
            }
        }
    }

    public void setTranslationMat(float transX, float transY)
    {
        setIdentity();
        Entries[0, 2] = transX;
        Entries[1, 2] = transY;
    }

    public void setRotationMat(float rotDeg)
    {
        setIdentity() ;
        float rad = rotDeg * Mathf.Deg2Rad;
        Entries[0,0] = Mathf.Cos(rad);
        Entries[0,1] = (Mathf.Sin(rad) * -1);
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
