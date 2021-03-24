using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshVert : MonoBehaviour
{
    public GameObject m_linePrefab;
    public List<meshVert> m_verts;
    public List<GameObject> m_lines;
    public bool m_fixed = false;
    public float m_heat = 0.0f;
    public float m_heatAbsorbtion = 0.5f;
    public Vector3 m_inputForce;
    public float m_storedForce;
    public float m_hitForce;

    public meshVert m_nextVert;

    private Vector3 m_storedPos;

    // Start is called before the first frame update
    void Start()
    {
        m_storedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != m_storedPos)
        {
            UpdateLines(true);
            m_storedPos = transform.position;
        }
    }

    public void ApplySquash(Vector3 input, float scalar)
    {
        Vector3 motion = Vector3.Normalize(input) * scalar * m_storedForce;

        List<float> l_dots = new List<float>();
        float total = 1;
        foreach(meshVert vert in m_verts)
        {
            float angle = Vector3.Dot(input, vert.transform.position - transform.position);
            l_dots.Add(angle);
            if(angle > 0) total += angle;
        }

        for(int i = 0; i < l_dots.Count; i++)
        {
            if(l_dots[i] > 0) m_verts[i].ApplySquash(input, scalar);
        }

        transform.Translate(motion);
        m_storedForce = 0;
        UpdateLines(true);
    }

    public void CalcForceSplit(Vector3 input, float force)
    {
        //First we take away the heat variable
        float subtract = force * m_heatAbsorbtion;
        m_heat += subtract;
        float newForce = force - subtract;
        //m_storedForce += newForce;
        

        List<float> l_dots = new List<float>();
        float total = 1;
        foreach(meshVert vert in m_verts)
        {
            float angle = Vector3.Dot(input, vert.transform.position - transform.position);
            l_dots.Add(angle);
            if(angle > 0) total += angle;
        }

        for(int i = 0; i < l_dots.Count; i++)
        {
            if(l_dots[i] > 0) m_verts[i].CalcForceSplit(input, newForce * (l_dots[i] / total));
        }
        m_storedForce += newForce * (1 / total);
        UpdateLines(false);
    }

    public void ConnectVertex(meshVert vert)
    {
        m_verts.Add(vert);
        GameObject newLine = Instantiate(m_linePrefab, transform.position, Quaternion.identity);
        newLine.transform.parent = transform;
        m_lines.Add(newLine);
        LineRenderer line = m_lines[m_lines.Count - 1].GetComponent<LineRenderer>();
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, m_verts[m_verts.Count - 1].transform.position - transform.position);
    }

    public void UpdateLines(bool pass)
    {
        for(int i = 0; i < m_verts.Count; i++)
        {
            m_lines[i].GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
            m_lines[i].GetComponent<LineRenderer>().SetPosition(1, m_verts[i].transform.position - transform.position);
        }
        if(pass) 
        {
            foreach(meshVert vert in m_verts)
            {
                vert.UpdateLines(false);
            }
        }
    }
}
