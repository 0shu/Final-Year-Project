using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLocator : MonoBehaviour
{
    public Selected m_mode = Selected.Hammer;
    public GameObject[] m_hotbar = new GameObject[4];
    //public bool m_hitter = true;
    public VolumeCalculator m_calc;
    public LayerMask m_self;
    public GameObject m_pointer;

    public Color[] m_types = new Color[4];
    public string m_measurable;
    public GameObject m_target;
    public int m_hitIndex;
    public Vector3 m_position = new Vector3(0, 0, 0);
    public Vector3 m_normal = new Vector3(0, 1, 0);
    public float m_temperature = 100.0f;

    public Text m_heat;

    private float m_scrollAmt = 0.0f;
    public float m_threshHold = 0.5f;

    public bool m_pause = false;
    public GameObject m_haltScreen;

    void Start()
    {
        m_calc = GetComponent<VolumeCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            SetPointerColors(Selected.Tongs);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            SetPointerColors(Selected.Hammer);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            SetPointerColors(Selected.Torch);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            SetPointerColors(Selected.Camera);
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SetPause(!m_pause);
        }

        //if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) 
        if(!m_pause) RaycastMouseHit();

        if (Input.GetMouseButtonDown(0) && m_target.tag == m_measurable) 
        {
            switch(m_mode)
            {
                case Selected.Tongs:
                    m_target.transform.SetParent(transform.GetChild(0));
                    Rigidbody rb = m_target.GetComponent<Rigidbody>();
                    if(rb) 
                    {
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }
                    break;

                case Selected.Hammer: //Applies hit depending on what type of hit taker is present
                    if(m_target.GetComponent<SimpleMesh>() != null) m_target.GetComponent<SimpleMesh>().ApplyHit(m_position, m_normal);
                    else if(m_target.GetComponent<SegmentedBar>() != null) m_target.GetComponent<SegmentedBar>().TakeHit(m_position, m_normal);
                    else if(m_target.GetComponent<SimpleTetrahedron>() != null) m_target.GetComponent<SimpleTetrahedron>().ApplyHit(m_position, -m_normal);
                    else if(m_target.GetComponent<SmashMesh>() != null) m_target.GetComponent<SmashMesh>().ApplyHit(m_position, -m_normal, m_hitIndex);
                    break;

                case Selected.Torch:

                    break;

                case Selected.Camera: //Saves mesh to an OBJ
                    Mesh mesh = m_target.GetComponent<MeshFilter>().sharedMesh;
                    if(mesh) MeshSaver.SaveToOBJ(m_target.name, mesh);
                    break;

                default:
                    Debug.LogWarning("Somehow you dont have anything selected on the hotbar...");
                    break;
            }

            m_calc.CalcVolume(m_target.GetComponent<MeshFilter>().mesh);
        }

        if (Input.GetMouseButtonUp(0) && m_target.transform.IsChildOf(transform.GetChild(0)))
        {
            m_target.transform.SetParent(null);
            Rigidbody rb = m_target.GetComponent<Rigidbody>();
            if(rb) 
            {
                rb.sleepThreshold = 0.0f;
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }

        if (m_mode == Selected.Torch && Input.GetMouseButton(0) && m_target.tag == m_measurable) 
        {
            if(m_target.GetComponent<HeatGlow>() != null) m_target.GetComponent<HeatGlow>().Heat(Time.deltaTime * m_temperature);
        }

        m_scrollAmt -= Input.mouseScrollDelta.y;
        if(m_scrollAmt >= m_threshHold) {
            m_scrollAmt -= m_threshHold;
            switch(m_mode)
            {
                case Selected.Tongs:
                    SetPointerColors(Selected.Hammer);
                    break;
                case Selected.Hammer:
                    SetPointerColors(Selected.Torch);
                    break;
                case Selected.Torch:
                    SetPointerColors(Selected.Camera);
                    break;
                case Selected.Camera:
                    SetPointerColors(Selected.Tongs);
                    break;
            }
        }
        else if(m_scrollAmt <= -m_threshHold) {
            m_scrollAmt += m_threshHold;
            switch(m_mode)
            {
                case Selected.Tongs:
                    SetPointerColors(Selected.Camera);
                    break;
                case Selected.Hammer:
                    SetPointerColors(Selected.Tongs);
                    break;
                case Selected.Torch:
                    SetPointerColors(Selected.Hammer);
                    break;
                case Selected.Camera:
                    SetPointerColors(Selected.Torch);
                    break;
            }
        }
    }

    void RaycastMouseHit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 999f, ~m_self))
        {
            m_position = hit.point;
            m_normal = hit.normal;
            m_hitIndex = hit.triangleIndex;
            m_pointer.transform.position = m_position;
            SetLinePoses();

            //check if its a new object we werent already looking at
            if(hit.collider.transform.gameObject != m_target)
            {
                m_target = hit.collider.transform.gameObject;

                if(m_target.tag == m_measurable) m_calc.CalcVolume(m_target.GetComponent<MeshFilter>().mesh);
                else m_calc.ResetText();
            }

            if(m_target.tag == m_measurable) m_heat.text = m_target.GetComponent<HeatGlow>().m_heat.ToString("F0") + "K";
            else m_heat.text = "";
        }
        
    }

    void SetLinePoses() {
        LineRenderer line = m_pointer.GetComponent<LineRenderer>();
        var points = new Vector3[2];
        points[0] = new Vector3(0, 0, 0);
        points[1] = m_normal * 3;

        line.SetPositions(points);
    }

    void SetPointerColors(Selected mode) {
        int old = (int) m_mode;
        m_mode = mode;
        int num = (int) mode;

        Material mat = m_pointer.GetComponent<MeshRenderer>().material;
        mat.SetColor("_Color", m_types[num]);
        mat.SetColor("_EmissionColor", m_types[num] * 0.5f);

        LineRenderer line = m_pointer.GetComponent<LineRenderer>();
        line.material = mat;

        m_hotbar[num].GetComponent<Image>().color = new Color(m_types[num].r, m_types[num].g, m_types[num].b, 1.0f);
        m_hotbar[num].GetComponent<RectTransform>().sizeDelta = new Vector2 (200, 200);
        m_hotbar[old].GetComponent<Image>().color = Color.black;
        m_hotbar[old].GetComponent<RectTransform>().sizeDelta = new Vector2 (150, 150);
    }

    public void SetPause(bool pause)
    {
        m_pause = pause;
        m_haltScreen.SetActive(pause);
        GetComponent<PlayerController>().SetPause(pause);
    }
}

public enum Selected {Tongs, Hammer, Torch, Camera};