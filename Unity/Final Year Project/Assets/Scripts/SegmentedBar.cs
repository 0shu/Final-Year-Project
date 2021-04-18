using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
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
        List<int> indices = new List<int>();

        foreach(BarSegment segment in m_segments)
        {
            verts.Add(new Vector3(segment.transform.position.x, segment.transform.position.y - segment.m_size.y, segment.transform.position.z - segment.m_size.z));
            verts.Add(new Vector3(segment.transform.position.x, segment.transform.position.y - segment.m_size.y, segment.transform.position.z + segment.m_size.z));
            verts.Add(new Vector3(segment.transform.position.x, segment.transform.position.y + segment.m_size.y, segment.transform.position.z + segment.m_size.z));
            verts.Add(new Vector3(segment.transform.position.x, segment.transform.position.y + segment.m_size.y, segment.transform.position.z - segment.m_size.z));
        }


        //Initial front face
        indices.Add(0);
        indices.Add(1);
        indices.Add(2);
        indices.Add(0);
        indices.Add(2);
        indices.Add(3);

        //Intermediate
        for(int i = 1 ; i < m_segments.Count; i++)
        {
            int start = (i * 4) - 4;

            indices.Add(start + 0);
            indices.Add(start + 4);
            indices.Add(start + 1);

            indices.Add(start + 4);
            indices.Add(start + 5);
            indices.Add(start + 1);

            indices.Add(start + 1);
            indices.Add(start + 5);
            indices.Add(start + 2);

            indices.Add(start + 5);
            indices.Add(start + 6);
            indices.Add(start + 2);

            indices.Add(start + 2);
            indices.Add(start + 6);
            indices.Add(start + 3);

            indices.Add(start + 6);
            indices.Add(start + 7);
            indices.Add(start + 3);

            indices.Add(start + 3);
            indices.Add(start + 7);
            indices.Add(start + 0);

            indices.Add(start + 7);
            indices.Add(start + 4);
            indices.Add(start + 0);
        }

        int amt = verts.Count - 4;

        //Final back face
        indices.Add(amt + 0);
        indices.Add(amt + 2);
        indices.Add(amt + 1);
        indices.Add(amt + 0);
        indices.Add(amt + 3);
        indices.Add(amt + 2);


        m_mesh.vertices = verts.ToArray();
        m_mesh.triangles = indices.ToArray();
    }
}
