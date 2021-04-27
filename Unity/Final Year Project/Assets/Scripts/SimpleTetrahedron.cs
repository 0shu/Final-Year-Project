using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class SimpleTetrahedron : MonoBehaviour
{
    public float volume;

    public Vector3[] m_vertices;

    Vector3 face1;
    Vector3 face2;
    Vector3 face3;
    Vector3 face4;

    public int[] indices = {0,1,2, 3,4,5, 6,7,8, 9,10,11};

    public Vector3[] vertPos;
    public Vector2[] vertUV;
    public Vector3[] vertNorm;

    int m_selected = 0;
    [Range(0.0f, 2.0f)]
    public float m_power = 0.91f;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyHit(Vector3 pos, Vector3 norm)
    {
        int vert = 0;
        float distance = Vector3.Distance(pos, m_vertices[0]);

        for(int i = 1; i < 4; i++)
        {
            if(Vector3.Distance(pos, m_vertices[i]) < distance)
            {
                distance = Vector3.Distance(pos, m_vertices[i]);
                vert = i;
            }
        }

        m_selected = vert;
        ApplyHit3D(norm, m_power);
        Generate();
    }

    public void ApplyHit3D(Vector3 direction, float maxForce)
    {
        //Decide which point based on the m_selected
        int zero = 0, one = 1, two = 2, three = 3;
        switch(m_selected)
        {
            case 0:
            {
                zero = 0;
                one = 1;
                two = 2;
                three = 3;
                break;
            }
            case 1:
            {
                zero = 1;
                one = 0;
                two = 2;
                three = 3;
                break;
            }
            case 2:
            {
                zero = 2;
                one = 0;
                two = 1;
                three = 3;
                break;
            }
            case 3:
            {
                zero = 3;
                one = 0;
                two = 1;
                three = 2;
                break;
            }
        }

        //Get normal of the plane of verts 1-3
        Vector3 side1 = m_vertices[two] - m_vertices [one];
        Vector3 side2 = m_vertices[three] - m_vertices [one];
        Vector3 normal = Vector3.Normalize(Vector3.Cross(side1, side2));

        Vector3 force = (maxForce * Vector3.Normalize(direction));

        //Get distance to a point on that plane
        Vector3 hyp1 = m_vertices[zero] - m_vertices[one];
        //Dot them, projecting one onto the other and get the projected distance of the hyp1
        float dist1 = Vector3.Dot(normal, hyp1) / normal.magnitude;

        //Move the vertex by the force variable
        m_vertices[zero] += force;

        //Get distance to a point on that plane
        Vector3 hyp2 = m_vertices[zero] - m_vertices[one];
        //Dot them, projecting one onto the other and get the projected distance of the hyp2
        float dist2 = Vector3.Dot(normal, hyp2) / normal.magnitude;

        //Get how much to change bottom proportionally
        float rootDiff = Mathf.Sqrt(dist1/dist2);

        //Get the center point of the other 3
        Vector3 center = (m_vertices[one] + m_vertices[two] + m_vertices[three]) / 3.0f;

        m_vertices[one] = center + (rootDiff * (m_vertices[one] - center));
        m_vertices[two] = center + (rootDiff * (m_vertices[two] - center));
        m_vertices[three] = center + (rootDiff * (m_vertices[three] - center));

    }

    void Generate()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.Clear();

        vertPos[0] = m_vertices[0];
        vertPos[1] = m_vertices[3];
        vertPos[2] = m_vertices[1];

        vertPos[3] = m_vertices[1];
        vertPos[4] = m_vertices[3];
        vertPos[5] = m_vertices[2];

        vertPos[6] = m_vertices[2];
        vertPos[7] = m_vertices[3];
        vertPos[8] = m_vertices[0];

        vertPos[9] = m_vertices[1];
        vertPos[10] = m_vertices[2];
        vertPos[11] = m_vertices[0];

        face1 = Vector3.Cross((m_vertices[3] - m_vertices[0]), (m_vertices[1] - m_vertices[0]));
        face2 = Vector3.Cross((m_vertices[3] - m_vertices[1]), (m_vertices[2] - m_vertices[1]));
        face3 = Vector3.Cross((m_vertices[2] - m_vertices[0]), (m_vertices[3] - m_vertices[0]));
        face4 = Vector3.Cross((m_vertices[1] - m_vertices[0]), (m_vertices[2] - m_vertices[0]));

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

        volume = Vector3.Dot(Vector3.Cross((m_vertices[1] - m_vertices[0]), (m_vertices[3] - m_vertices[0])), (m_vertices[2] - m_vertices[0])) / 6.0f;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
