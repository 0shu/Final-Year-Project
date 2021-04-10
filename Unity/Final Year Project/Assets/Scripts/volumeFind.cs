using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tetra 
{
    public Vector3 a; //First vertex of face
    public Vector3 b; //Next vertex of face
    public Vector3 c; //Final vertex of face
    public float vol; //Output volume of the tetrahedron
}

public class volumeFind : MonoBehaviour
{
    public ComputeShader m_compute;
    public Mesh m_mesh;
    public Vector3 m_origin = new Vector3();
    public float m_total = 0;
    public float m_arraysVol = 0;
    public int m_faces = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {    
        VolWithArrays();
    }

    public void VolWithArrays()
    {
        Debug.Log("Starting to get mesh!");
        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;

        Debug.Log("Verts: " + verts.Length);
        Debug.Log("Tris: " + indices.Length);

        //int amt = Mathf.CeilToInt(indices.Length / 3.0f);
        float[] volumes = new float[indices.Length];
        for (int i = 0; i < volumes.Length; i++)
        {
            volumes[i] = 0;
        }

        ComputeBuffer vertBuffer = new ComputeBuffer(verts.Length, sizeof(float) * 3);
        ComputeBuffer indiceBuffer = new ComputeBuffer(indices.Length, sizeof(int));
        ComputeBuffer volumeBuffer = new ComputeBuffer(volumes.Length, sizeof(float));
        vertBuffer.SetData(verts);
        indiceBuffer.SetData(indices);
        volumeBuffer.SetData(volumes);

        float[] origin = {m_origin.x, m_origin.y, m_origin.z};
        m_compute.SetFloats("origin", origin);
        m_compute.SetBuffer(0, "positions", vertBuffer);
        m_compute.SetBuffer(0, "indices", indiceBuffer);
        m_compute.SetBuffer(0, "volumes", volumeBuffer);
        m_compute.Dispatch(0, Mathf.CeilToInt(indices.Length / 30.0f), 1, 1);

        volumeBuffer.GetData(volumes);


        m_arraysVol = 0.0f;
        for (int i = 0; i < volumes.Length; i++)
        {
            m_arraysVol += volumes[i];
        }

        Debug.Log("Finished Calculating Volume! " + m_arraysVol);
        vertBuffer.Dispose();
        indiceBuffer.Dispose();
        volumeBuffer.Dispose();
    }
}
