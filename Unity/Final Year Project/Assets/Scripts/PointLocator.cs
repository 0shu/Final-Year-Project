using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLocator : MonoBehaviour
{
    public LayerMask m_self;
    public GameObject m_pointer;
    public Vector3 m_position = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) RaycastMouseHit();
    }

    void RaycastMouseHit()
    {
        Debug.Log("Starting raycast!");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 999f, ~m_self))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            Debug.Log("Hit Detected! " + hit.point.x + " " + hit.point.y + " " + hit.point.z);

            m_position = hit.point;
            m_pointer.transform.position = m_position;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
        }
    }
}
