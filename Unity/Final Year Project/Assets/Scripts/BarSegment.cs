using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSegment : MonoBehaviour
{
    public bool m_hit = false;
    public int m_index = 0;
    public SegmentedBar m_parent;
    public Vector3 m_size = new Vector3(0.5f, 0.5f, 0.5f);
    public float m_factor = 1.1f;

    void Update()
    {
        if(m_hit)
        {
            m_hit = false;

            float root = Mathf.Pow(m_factor, 0.5f);
            scale(new Vector3(root, 1.0f / m_factor, root));
        }
    }

    public void scale(Vector3 scale)
    {
        m_size = new Vector3(m_size.x * scale.x, m_size.y * scale.y, m_size.z * scale.z);
        m_parent.UpdateAround(m_index);
    }
}
