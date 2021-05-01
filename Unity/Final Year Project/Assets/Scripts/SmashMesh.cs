using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class SmashMesh : MonoBehaviour
{
    public float m_power = 0.15f;
    
    public Mesh m_mesh;

    void Start()
    {
        if(m_mesh)
        {
            GetComponent<MeshFilter>().mesh = m_mesh;
            GetComponent<MeshCollider>().sharedMesh = m_mesh;
        }
        else if(GetComponent<MeshFilter>().mesh != null)
        {
            m_mesh = GetComponent<MeshFilter>().mesh;
            GetComponent<MeshCollider>().sharedMesh = m_mesh;
        }
    }

    public void ApplyHit(Vector3 pos, Vector3 normal, int index)
    {
        Vector3 localPos = transform.InverseTransformPoint(pos);
        m_mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;

        Vector3 center = new Vector3(0, 0, 0);

        for(int i = 0; i < indices.Length; i++)
        {
            center += verts[indices[i]];
        }

        center /= indices.Length;

        for(int i = 0; i < 3; i++)
        {
            Vector3 offset = verts[indices[(index * 3) + i]] - localPos;
            offset = offset + (offset * m_power) + (normal * m_power);

            verts[indices[(index * 3) + i]] = offset + localPos;
        }

        // Do some calculations...
        m_mesh.vertices = verts;

        GetComponent<MeshCollider>().sharedMesh = m_mesh;
    }
}