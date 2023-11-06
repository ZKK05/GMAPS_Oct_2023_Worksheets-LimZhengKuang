using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour

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
        HMatrix2D mat1 = new HMatrix2D(1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f);
        HVector2D vec1 = new HVector2D(1f, 2f);
        HVector2D result = new HVector2D();
        result = mat1 * vec1;
        print(result.x + " " + result.y);
    }

  
}
