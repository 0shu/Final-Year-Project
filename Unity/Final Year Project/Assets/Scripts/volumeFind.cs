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
    public float[] m_vols = new float[20];
    public float m_total = 0;
    public float m_arraysVol = 0;
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
        //VolOnGPU();
        VolWithArrays();
    }

    public void VolWithArrays()
    {
        Debug.Log("Starting to get mesh!");
        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;
        float[] volumes = new float[indices.Length];

        ComputeBuffer vertBuffer = new ComputeBuffer(verts.Length, sizeof(float) * 3);
        ComputeBuffer indiceBuffer = new ComputeBuffer(indices.Length, sizeof(int));
        ComputeBuffer volumeBuffer = new ComputeBuffer(volumes.Length, sizeof(float));
        vertBuffer.SetData(verts);
        indiceBuffer.SetData(indices);

        float[] origin = {m_origin.x, m_origin.y, m_origin.z};
        m_compute.SetFloats("origin", origin);
        m_compute.SetBuffer(0, "positions", vertBuffer);
        m_compute.SetBuffer(0, "indices", vertBuffer);
        m_compute.SetBuffer(0, "volumes", volumeBuffer);
        m_compute.Dispatch(0, Mathf.CeilToInt(indices.Length / 30.0f), 1, 1);

        volumeBuffer.GetData(volumes);


        m_arraysVol = 0.0f;
        for (int i = 0; i < volumes.Length; i++)
        {
            m_arraysVol += volumes[i];
        }

        vertBuffer.Dispose();
        indiceBuffer.Dispose();
        volumeBuffer.Dispose();
        Debug.Log("Finished Calculating Volume!");
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
        m_compute.Dispatch(0, Mathf.CeilToInt(m_tetras.Count / 10.0f), 1, 1);

        Tetra[] newTetras = new Tetra[m_faces];
        tetraBuffer.GetData(newTetras);


        m_total = 0.0f;
        for (int i = 0; i < newTetras.Length; i++)
        {
            m_total += newTetras[i].vol;
            m_vols[i] = newTetras[i].vol;
            newTetras[i].vol = 0;
        }
        m_tetras = new List<Tetra>(newTetras);

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
