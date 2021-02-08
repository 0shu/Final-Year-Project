using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public Animator m_anim;
    public GameObject m_sparks;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_anim.Play("Hit");
            m_sparks.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            m_sparks.SetActive(false);
        }
    }

}
