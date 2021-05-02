using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SimpleMesh : MonoBehaviour
{
    public float m_compression = 0.5f;
    
    public Mesh m_mesh;

    public void ApplyHit(Vector3 pos, Vector3 normal)
    {
        Vector3 localPos = transform.InverseTransformPoint(pos);
        Vector3 localNorm = transform.InverseTransformVector(normal);
        m_mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = m_mesh.vertices;
        int[] indices = m_mesh.triangles;

        Vector3 center = new Vector3(0, 0, 0);

        for(int i = 0; i < indices.Length; i++)
        {
            center += verts[indices[i]];
        }

        center /= indices.Length;

        float force = 1.0f + (m_compression * GetComponent<HeatGlow>().GetPercent());

        float newOne = (1 / force);
        float newTwo = (Mathf.Pow(force, 0.5f));
        Vector3 newScale;

        if(localNorm == Vector3.up || localNorm == -Vector3.up) newScale = new Vector3(newTwo, newOne, newTwo);
        else if(localNorm == Vector3.right || localNorm == -Vector3.right) newScale = new Vector3(newOne, newTwo, newTwo);
        else if(localNorm == Vector3.forward || localNorm == -Vector3.forward) newScale = new Vector3(newTwo, newTwo, newOne);
        else
        {
            Debug.Log("Normal is not on the axes!");

            Matrix4x4 rot = Matrix4x4.LookAt(Vector3.zero, localNorm, Vector3.up);
            Matrix4x4 inv = rot.inverse;

            for(int i = 0; i < verts.Length; i++)
            {
                Vector3 difference = verts[i] - center;
                Vector3 point = inv.MultiplyPoint3x4(difference);
                point = new Vector3(point.x * newTwo, point.y * newTwo, point.z * newOne);
                verts[i] = center + rot.MultiplyPoint3x4(point);
            }

            m_mesh.vertices = verts;
            MeshCollider colli = GetComponent<MeshCollider>();
            colli.sharedMesh = m_mesh;

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
