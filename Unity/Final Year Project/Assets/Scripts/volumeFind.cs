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
    public List<Tetra> m_tetras = new List<Tetra>();
    public Mesh m_mesh;
    public Vector3 m_origin = new Vector3();
    public float m_total = 0;
    public int m_faces = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetMeshAsTetras();
        VolOnGPU();
    }

    // Update is called once per frame
    void Update()
    {    
        VolOnGPU();
    }

    public void VolOnGPU()
    {
        Debug.Log("Starting to calc volume!");
        int totalSize = sizeof(float) * 10;
        ComputeBuffer tetraBuffer = new ComputeBuffer(m_tetras.Count, totalSize);
        tetraBuffer.SetData(m_tetras.ToArray());

        float[] origin = {m_origin.x, m_origin.y, m_origin.z};
        m_compute.SetBuffer(0, "tetras", tetraBuffer);
        m_compute.SetFloats("origin", origin);
        m_compute.Dispatch(0, m_tetras.Count / 10, 1, 1);

        Tetra[] newTetras = new Tetra[m_faces];
        tetraBuffer.GetData(newTetras);

        m_tetras = new List<Tetra>(newTetras);

        m_total = 0.0f;
        for (int i = 0; i < m_tetras.Count; i++)
        {
            m_total += newTetras[i].vol;
        }

        tetraBuffer.Dispose();
        Debug.Log("Finished Calculating Volume!");
    }

    public void GetMeshAsTetras()
    {
        Debug.Log("Starting to get mesh!");
        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;

        Debug.Log("Verts: " + verts.Length);
        Debug.Log("Tris: " + indices.Length);

        if( m_tetras.Count > 0) m_tetras.RemoveRange(0, m_tetras.Count);

        for(int i = 0; i <= indices.Length - 3; i+=3)
        {
            Tetra tetra;
            tetra.a = verts[indices[i]];
            tetra.b = verts[indices[i+1]];
            tetra.c = verts[indices[i+2]];
            tetra.vol = 0.0f;

            m_tetras.Add(tetra);
        }
        Debug.Log("Finished getting mesh!");

        m_faces = m_tetras.Count;
    }
}
