using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour //this script so just to test some early formulas and functions

{
    private HMatrix2D mat = new HMatrix2D();
    private float result;
    
    void Start()
    {
        mat.setIdentity();
        mat.Print();
        Question2D();
    }

    void Question2D()
    {
        HMatrix2D mat1 = new HMatrix2D(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f); //this test was for question 2D, 
        HVector2D vec1 = new HVector2D(1f, 2f);//tested whether the multipcation of vector and a matrix works or not
        HVector2D result = new HVector2D();
        result = mat1 * vec1;
        print(result.x + " " + result.y);
    }

  
}
