using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class SegmentedBar : MonoBehaviour
{
    public Mesh m_mesh; //Mesh to draw the solid
    public List<BarSegment> m_segments; //Segments that make up the bar


    // Start is called before the first frame update
    void Start()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;

        foreach (Transform child in transform)
        {
            if(child.GetComponent<BarSegment>() != null) m_segments.Add(child.GetComponent<BarSegment>());
        }
        //Make sure all the segments are index correctly
        for(int i = 0; i < m_segments.Count; i++)
        {
            m_segments[i].m_index = i;
        }

        //Update it to lay it out correctly
        UpdateAround(m_segments.Count / 2);
    }

    public void TakeHit(Vector3 pos, Vector3 norm)
    {
        BarSegment closest = new BarSegment();
        float distance = 0;

        Vector3 newNorm = transform.worldToLocalMatrix * norm;

        //Go through and find closest segment to hit
        foreach(BarSegment segment in m_segments)
        {
            if(distance == 0 || distance > Vector3.Distance(pos, segment.transform.position))
            {
                closest = segment;
                distance = Vector3.Distance(pos, segment.transform.position);
            }
        }

        //Tell closest to recieve hit
        closest.ApplyHit(newNorm, 1.0f);
    }

    public void UpdateAround(int index)
    {
        if(m_segments.Count <= index)
        {
            Debug.Log("Not enough elements in bar for suggested update!");
            return;
        }
        else
        {
            //Set this parent objects origin to the position of the segment
            transform.position = m_segments[index].gameObject.transform.TransformPoint(Vector3.zero);

            //Set segment back to origin
            m_segments[index].gameObject.transform.localPosition = Vector3.zero;

            float pos = -m_segments[index].m_size.x;
            //count down from index
            for(int i = index -1; i >= 0; i--)
            {
                pos -= m_segments[i].m_size.x;
                m_segments[i].gameObject.transform.localPosition = new Vector3(pos, 0, 0);
                pos -= m_segments[i].m_size.x;
            }

            pos = m_segments[index].m_size.x;
            //count up from index
            for(int i = index +1; i < m_segments.Count; i++)
            {
                pos += m_segments[i].m_size.x;
                m_segments[i].gameObject.transform.localPosition = new Vector3(pos, 0, 0);
                pos += m_segments[i].m_size.x;
            }

            ConstructMesh();
        }
    }

    public void ConstructMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> actual = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> indices = new List<int>();

        foreach(BarSegment segment in m_segments)
        {
            verts.Add(new Vector3(segment.transform.localPosition.x, segment.transform.localPosition.y - segment.m_size.y, segment.transform.localPosition.z - segment.m_size.z));
            verts.Add(new Vector3(segment.transform.localPosition.x, segment.transform.localPosition.y - segment.m_size.y, segment.transform.localPosition.z + segment.m_size.z));
            verts.Add(new Vector3(segment.transform.localPosition.x, segment.transform.localPosition.y + segment.m_size.y, segment.transform.localPosition.z + segment.m_size.z));
            verts.Add(new Vector3(segment.transform.localPosition.x, segment.transform.localPosition.y + segment.m_size.y, segment.transform.localPosition.z - segment.m_size.z));
        }


        //Initial front face
        actual.Add(verts[0]);
        actual.Add(verts[1]);
        actual.Add(verts[2]);

        actual.Add(verts[0]);
        actual.Add(verts[2]);
        actual.Add(verts[3]);



        //Intermediate
        for(int i = 1 ; i < m_segments.Count; i++)
        {
            int start = (i * 4) - 4;

            actual.Add(verts[start + 0]);
            actual.Add(verts[start + 4]);
            actual.Add(verts[start + 1]);

            actual.Add(verts[start + 1]);
            actual.Add(verts[start + 4]);
            actual.Add(verts[start + 5]);

            actual.Add(verts[start + 1]);
            actual.Add(verts[start + 5]);
            actual.Add(verts[start + 2]);

            actual.Add(verts[start + 2]);
            actual.Add(verts[start + 5]);
            actual.Add(verts[start + 6]);

            actual.Add(verts[start + 2]);
            actual.Add(verts[start + 6]);
            actual.Add(verts[start + 3]);

            actual.Add(verts[start + 3]);
            actual.Add(verts[start + 6]);
            actual.Add(verts[start + 7]);

            actual.Add(verts[start + 3]);
            actual.Add(verts[start + 7]);
            actual.Add(verts[start + 0]);

            actual.Add(verts[start + 0]);
            actual.Add(verts[start + 7]);
            actual.Add(verts[start + 4]);
        }

        int amt = verts.Count - 4;

        //Final back face
        actual.Add(verts[amt + 0]);
        actual.Add(verts[amt + 2]);
        actual.Add(verts[amt + 1]);

        actual.Add(verts[amt + 0]);
        actual.Add(verts[amt + 3]);
        actual.Add(verts[amt + 2]);

        foreach(Vector3 pos in actual)
        {
            indices.Add(indices.Count);
        }

        for(int i = 0; i < (actual.Count / 3.0); i++)
        {
            int posi = i * 3;

            Vector3 edgeA = actual[posi + 2] - actual[posi];
            Vector3 edgeB = actual[posi + 1] - actual[posi];

            Vector3 norm = Vector3.Cross(edgeA, edgeB);

            normals.Add(norm);
            normals.Add(norm);
            normals.Add(norm);
        }

        m_mesh.vertices = actual.ToArray();
        m_mesh.triangles = indices.ToArray();
        m_mesh.normals = normals.ToArray();

        GetComponent<MeshCollider>().sharedMesh = m_mesh;
    }
}
