using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSegment : MonoBehaviour
{
    public SegmentedBar m_parent;
    public Vector3 m_size = new Vector3(1, 1, 1);

    public void scale(Vector3 scale)
    {
        m_size = new Vector3(m_size.x * scale.x, m_size.y * scale.y, m_size.z * scale.z);
    }
}
