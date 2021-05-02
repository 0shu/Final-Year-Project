using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGlow : MonoBehaviour
{
    public Color m_color;
    public Color m_affectColor;

    const float m_minHeat = 300.0f;
    const float m_maxHeat = 1800.0f;

    [Range(m_minHeat, m_maxHeat)]
    public float m_heat = 300.0f;
    public float m_coolRate = 5.0f;

    public Material m_material;
    public Gradient m_gradient = new Gradient();

    public GradientColorKey[] m_colorKey;
    public GradientAlphaKey[] m_alphaKey;

    void Start() {
        if (m_material != null) m_material = new Material(m_material);
        m_material.name = "new" + m_material.name;
        //else m_material = new Material();

        SetUpColours();
        GetComponent<MeshRenderer>().material = m_material;
    }

    void Update() {
        m_heat = Mathf.Max(m_minHeat, m_heat - (Time.deltaTime * m_coolRate));
        m_color = m_gradient.Evaluate(m_heat/m_maxHeat);
        m_affectColor = new Color(Mathf.Max(m_color.r - m_colorKey[0].color.r, 0), Mathf.Max(m_color.g - m_colorKey[0].color.g, 0), Mathf.Max(m_color.b - m_colorKey[0].color.b, 0)) * 0.5f;
        m_material.SetColor("_EmissionColor", m_affectColor);
        if(GetComponent<Light>() != null) GetComponent<Light>().color = m_affectColor * 0.5f;
    }

    void SetUpColours()
    {
        m_colorKey = new GradientColorKey[6];
        m_alphaKey = new GradientAlphaKey[6];

        m_colorKey[0].color = new Color(0.2f, 0.2f, 0.2f);
        m_colorKey[0].time = 500.0f / m_maxHeat;
        m_alphaKey[0].alpha = 1.0f;
        m_alphaKey[0].time = 500.0f / m_maxHeat;

        m_colorKey[1].color = new Color(0.5f, 0.0f, 0.0f);
        m_colorKey[1].time = 770.0f / m_maxHeat;
        m_alphaKey[1].alpha = 1.0f;
        m_alphaKey[1].time = 770.0f / m_maxHeat;

        m_colorKey[2].color = new Color(0.9f, 0.0f, 0.0f);
        m_colorKey[2].time = 970.0f / m_maxHeat;
        m_alphaKey[2].alpha = 1.0f;
        m_alphaKey[2].time = 970.0f / m_maxHeat;

        m_colorKey[3].color = new Color(1.0f, 0.7f, 0.5f);
        m_colorKey[3].time = 1120.0f / m_maxHeat;
        m_alphaKey[3].alpha = 1.0f;
        m_alphaKey[3].time = 1120.0f / m_maxHeat;

        m_colorKey[4].color = new Color(1.0f, 1.0f, 0.25f);
        m_colorKey[4].time = 1250.0f / m_maxHeat;
        m_alphaKey[4].alpha = 1.0f;
        m_alphaKey[4].time = 1270.0f / m_maxHeat;

        m_colorKey[5].color = new Color(1.0f, 1.0f, 1.0f);
        m_colorKey[5].time = 1480.0f / m_maxHeat;
        m_alphaKey[5].alpha = 1.0f;
        m_alphaKey[5].time = 1480.0f / m_maxHeat;

        m_gradient.SetKeys(m_colorKey, m_alphaKey);
    }
}