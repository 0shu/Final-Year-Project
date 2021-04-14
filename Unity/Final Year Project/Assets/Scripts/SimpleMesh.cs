using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SimpleMesh : MonoBehaviour
{
    public float m_compression = 1.1f;
    
    public Mesh m_mesh;

    public void ApplyHit(Vector3 pos, Vector3 normal)
    {
        Vector3 localPos = pos - transform.position;
        m_mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;

        Vector3 center = new Vector3(0, 0, 0);

        for(int i = 0; i < indices.Length; i++)
        {
            center += verts[indices[i]];
        }

        center /= indices.Length;

        float newOne = (1 / m_compression);
        float newTwo = (Mathf.Pow(m_compression, 0.5f));
        Vector3 newScale;

        if(normal == Vector3.up || normal == -Vector3.up) newScale = new Vector3(newTwo, newOne, newTwo);
        else if(normal == Vector3.right || normal == -Vector3.right) newScale = new Vector3(newOne, newTwo, newTwo);
        else if(normal == Vector3.forward || normal == -Vector3.forward) newScale = new Vector3(newTwo, newTwo, newOne);
        else
        {
            Debug.Log("Normal is not on the axes!");
            return;
        }

        for(int i = 0; i < verts.Length; i++)
        {
            Vector3 difference = verts[i] - center;
            difference = new Vector3(difference.x * newScale.x, difference.y * newScale.y, difference.z * newScale.z);
            verts[i] = center + difference;
        }

        m_mesh.vertices = verts;
        MeshCollider col = GetComponent<MeshCollider>();
        col.sharedMesh = m_mesh;
    }
}
