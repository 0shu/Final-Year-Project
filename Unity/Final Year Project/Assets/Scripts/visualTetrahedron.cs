using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class visualTetrahedron : MonoBehaviour
{
    public List<Vector3> m_vertices = new List<Vector3>(4);
    public SelectedVert m_selected;
    [Range(0.0f, 2.0f)]
    public float m_hitforce = 0.91f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyHit1D()
    {
        //Decide which point based on the m_selected
        int zero = 0, one = 1, two = 2, three = 3;
        switch(m_selected)
        {
            case SelectedVert.A:
            {
                zero = 0;
                one = 1;
                two = 2;
                three = 3;
                break;
            }
            case SelectedVert.B:
            {
                zero = 1;
                one = 0;
                two = 2;
                three = 3;
                break;
            }
            case SelectedVert.C:
            {
                zero = 2;
                one = 0;
                two = 1;
                three = 3;
                break;
            }
            case SelectedVert.D:
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

        //Get distance to a point on that plane
        Vector3 hyp = m_vertices[zero] - m_vertices[one];

        //Dot them, projecting one onto the other and get the projected distance of the hyp
        float dist = Vector3.Dot(normal, hyp) / normal.magnitude;

        //Transform vert 0 along the plane normal
        float newDist = dist * (m_hitforce - 1);
        Vector3 move = normal * newDist;
        m_vertices[zero] += move;

        //Get the center point of the other 3
        Vector3 center = (m_vertices[one] + m_vertices[two] + m_vertices[three]) / 3.0f;

        float rootDiff = Mathf.Sqrt(1 / m_hitforce);

        m_vertices[one] = center + (rootDiff * (m_vertices[one] - center));
        m_vertices[two] = center + (rootDiff * (m_vertices[two] - center));
        m_vertices[three] = center + (rootDiff * (m_vertices[three] - center));

        SetUpLines();
    }

    public void SetUpLines()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        var points = new Vector3[8];
        //ABCADBCD = line order
        points[0] = m_vertices[0];
        points[1] = m_vertices[1];
        points[2] = m_vertices[2];
        points[3] = m_vertices[0];
        points[4] = m_vertices[3];
        points[5] = m_vertices[1];
        points[6] = m_vertices[2];
        points[7] = m_vertices[3];

        lineRenderer.SetPositions(points);
    }
}

public enum SelectedVert {A, B, C, D};