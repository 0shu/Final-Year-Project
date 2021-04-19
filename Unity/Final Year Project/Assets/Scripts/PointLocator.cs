using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLocator : MonoBehaviour
{
    public bool m_hitter = true;
    public VolumeCalculator m_calc;
    public LayerMask m_self;
    public GameObject m_pointer;
    public string m_measurable;
    public GameObject m_target;
    public Vector3 m_position = new Vector3(0, 0, 0);
    public Vector3 m_normal = new Vector3(0, 1, 0);

    void Start()
    {
        m_calc = GetComponent<VolumeCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) RaycastMouseHit();
        if (Input.GetMouseButtonDown(0) && m_target.tag == m_measurable) 
        {
            if(m_hitter) 
            {
                if(m_target.GetComponent<SimpleMesh>() != null) m_target.GetComponent<SimpleMesh>().ApplyHit(m_position, m_normal);
                else if(m_target.GetComponent<SegmentedBar>() != null) m_target.GetComponent<SegmentedBar>().TakeHit(m_position, m_normal);
            }
            m_calc.CalcVolume(m_target.GetComponent<MeshFilter>().mesh);
        }
    }

    void RaycastMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 999f, ~m_self))
        {
            m_position = hit.point;
            m_normal = hit.normal;
            m_pointer.transform.position = m_position;
            SetLinePoses();

            //check if its a new object we werent already looking at
            if(hit.collider.transform.gameObject != m_target)
            {
                m_target = hit.collider.transform.gameObject;

                if(m_target.tag == m_measurable) m_calc.CalcVolume(m_target.GetComponent<MeshFilter>().mesh);
                else m_calc.ResetText();
            }
        }
    }

    void SetLinePoses()
    {
        LineRenderer line = m_pointer.GetComponent<LineRenderer>();
        var points = new Vector3[2];
        //ABCADBCD = line order
        points[0] = new Vector3(0, 0, 0);
        points[1] = m_normal * 3;

        line.SetPositions(points);
    }
}
