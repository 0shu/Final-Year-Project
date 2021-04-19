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
            scale(new Vector3(root, 1 / m_factor, root));
        }
    }

    public void scale(Vector3 scale)
    {
        m_size = new Vector3(m_size.x * scale.x, m_size.y * scale.y, m_size.z * scale.z);
        m_parent.UpdateAround(m_index);
    }

    public void ApplyHit(Vector3 direction, float scalar)
    {
        Vector3 amount = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z));

        if(amount.x > amount.y && amount.x > amount.z)
        {
            float strength = amount.x * scalar * (m_factor - 1.0f) + 1.0f;
            float root = Mathf.Pow(strength, 0.5f);
            amount = new Vector3(1 / strength, root, root);
        }
        else if(amount.y > amount.x && amount.y > amount.z)
        {
            float strength = amount.y * scalar * (m_factor - 1.0f) + 1.0f;
            float root = Mathf.Pow(strength, 0.5f);
            amount = new Vector3(root, 1 / strength, root);
        }
        else if(amount.z > amount.x && amount.z > amount.y)
        {
            float strength = amount.z * scalar * (m_factor - 1.0f) + 1.0f;
            float root = Mathf.Pow(strength, 0.5f);
            amount = new Vector3(root, root, 1 / strength);
        }

        scale(amount);
    }
}
