using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class SimpleTetrahedron : MonoBehaviour
{
    public float volume;

    public Vector3 vertA;
    public Vector3 vertB;
    public Vector3 vertC;
    public Vector3 vertD;

    Vector3 face1;
    Vector3 face2;
    Vector3 face3;
    Vector3 face4;

    public int[] indices = {0,1,2, 3,4,5, 6,7,8, 9,10,11};

    public Vector3[] vertPos;
    public Vector2[] vertUV;
    public Vector3[] vertNorm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Generate();
    }

    void Generate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        vertPos[0] = vertA;
        vertPos[1] = vertD;
        vertPos[2] = vertB;

        vertPos[3] = vertB;
        vertPos[4] = vertD;
        vertPos[5] = vertC;

        vertPos[6] = vertC;
        vertPos[7] = vertD;
        vertPos[8] = vertA;

        vertPos[9] = vertB;
        vertPos[10] = vertC;
        vertPos[11] = vertA;

        face1 = Vector3.Cross((vertD - vertA), (vertB - vertA));
        face2 = Vector3.Cross((vertD - vertB), (vertC - vertB));
        face3 = Vector3.Cross((vertC - vertA), (vertD - vertA));
        face4 = Vector3.Cross((vertB - vertA), (vertC - vertA));

        vertNorm[0] = face1;
        vertNorm[1] = face1;
        vertNorm[2] = face1;
        vertNorm[3] = face2;
        vertNorm[4] = face2;
        vertNorm[5] = face2;
        vertNorm[6] = face3;
        vertNorm[7] = face3;
        vertNorm[8] = face3;
        vertNorm[9] = face4;
        vertNorm[10] = face4;
        vertNorm[11] = face4;
        

        // Do some calculations...
        mesh.vertices = vertPos;
        mesh.uv = vertUV;
        mesh.normals = vertNorm;
        mesh.triangles = indices;

        volume = Vector3.Dot(Vector3.Cross((vertB - vertA), (vertD - vertA)), (vertC - vertA)) / 6.0f;
    }
}
