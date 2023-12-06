using UnityEngine;

public class MeshManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    [HideInInspector]
    public Mesh originalMesh, clonedMesh;

    public Vector3[] vertices { get; private set; } //array to store vertices values
    public int[] triangles { get; private set; } //array to store triangle values

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>(); 
        originalMesh = meshFilter.sharedMesh; //Get org mesh from meshfilter

        clonedMesh = new Mesh(); //Create a new mesh called clone of the org mesh
        clonedMesh.name = "clone";

        clonedMesh.vertices = originalMesh.vertices; //Copy vertex, triangle, normal and uv data from org mesh
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.normals = originalMesh.normals;
        clonedMesh.uv = originalMesh.uv;

        meshFilter.mesh = clonedMesh; //set the meshfilter's mesh to cloned mesh so that we can manipulate the cloned mesh

        vertices = clonedMesh.vertices; //set vertices and triangle arrays so that it is easily accessed
        triangles = clonedMesh.triangles;
    }
}
