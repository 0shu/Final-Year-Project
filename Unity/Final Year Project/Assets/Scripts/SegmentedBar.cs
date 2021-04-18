using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentedBar : MonoBehaviour
{
    public List<BarSegment> m_segments;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Matrix4x4 mat = m_segments[index].gameObject.transform.localToWorldMatrix;
            transform.position = mat * Vector3.zero;
            //Set segment back to origin
            m_segments[index].gameObject.transform.position = Vector3.zero;

            float pos = -m_segments[index].m_size.x / 2.0f;
            //count down from index
            for(int i = index -1; i >= 0; i--)
            {
                pos -= m_segments[i].m_size.x / 2.0f;
                m_segments[i].gameObject.transform.position = new Vector3(pos, 0, 0);
                pos -= m_segments[i].m_size.x / 2.0f;
            }

            pos = m_segments[index].m_size.x / 2.0f;
            //count up from index
            for(int i = index +1; i < m_segments.Count; i++)
            {
                pos += m_segments[i].m_size.x / 2.0f;
                m_segments[i].gameObject.transform.position = new Vector3(pos, 0, 0);
                pos += m_segments[i].m_size.x / 2.0f;
            }
        }
    }
}
