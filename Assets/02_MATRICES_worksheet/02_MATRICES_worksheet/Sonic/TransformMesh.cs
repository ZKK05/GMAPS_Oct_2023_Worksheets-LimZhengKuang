

//using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformMesh : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] vertices { get; private set; }
   
    //homogenous transformation matrices for translation, rotation, and origin adjustment
    private HMatrix2D transformMatrix = new HMatrix2D();
    HMatrix2D toOriginMatrix = new HMatrix2D();
    HMatrix2D fromOriginMatrix = new HMatrix2D();
    HMatrix2D rotateMatrix = new HMatrix2D();

    private MeshManager meshManager;
    HVector2D pos = new HVector2D();

    private float translateX;
    private float translateY;


    void Start()
    {
        meshManager = GetComponent<MeshManager>();
        pos = new HVector2D(gameObject.transform.position.x, gameObject.transform.position.y);
        //initialize the homogeneous vector (object's position)

        translateX = 1;
        translateY = 1;
        Translate(translateX, translateY); //translate mesh
        Rotate(90); //set angle of rotation
        print(transform.position.x);
    }


    void Translate(float x, float y) 
    {
        transformMatrix.setIdentity(); //set transform matrix to identity matrix
        transformMatrix.setTranslationMat(x, y); //set translation matrix 
        Transform();

        pos = transformMatrix * pos;
    }

    void Rotate(float angle)
    {
        HMatrix2D toOriginMatrix = new HMatrix2D(); //create translate matrix for to & from, create rotate matrix 
        HMatrix2D fromOriginMatrix = new HMatrix2D();
        HMatrix2D rotateMatrix = new HMatrix2D();

        toOriginMatrix.setTranslationMat(-translateX, -translateY); //set translation matrix with the translate values previously set
        fromOriginMatrix.setTranslationMat(translateX, translateY);//same but for (fromOrigin) so it is positive

        rotateMatrix.setRotationMat(angle); //rotation matrix

        
        transformMatrix.setIdentity();
        transformMatrix = fromOriginMatrix * rotateMatrix * toOriginMatrix; //concatenate the rotation transformation with translation transform
        Transform(); //call transform
    }

    private void Transform()
    {
        vertices = meshManager.clonedMesh.vertices; //get the cloned mesh from the meshmanager

        for (int i = 0; i < vertices.Length; i++) //transform each vertex using homogenous transformation matrix
        {
            HVector2D vert = new HVector2D(vertices[i].x, vertices[i].y );
            vert = transformMatrix * vert;
            vertices[i].x = vert.x; //update the x, y coords of vertex
            vertices[i].y = vert.y;
        }

        meshManager.clonedMesh.vertices = vertices; //update cloned mesh vertices
    }
}
