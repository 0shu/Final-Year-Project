using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class visualTetrahedron : MonoBehaviour
{
    public List<Vector3> m_vertices = new List<Vector3>(4);
    public SelectedVert m_selected;

    // Start is called before the first frame update
    void Start()
    {
        SetUpLines();
    }

    // Update is called once per frame
    void Update()
    {
        
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