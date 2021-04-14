using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCalculator : MonoBehaviour
{
    public Text m_text;
    public Mesh m_mesh;
    public Vector3 m_origin = new Vector3();
    public float m_volume = 0;

    // Start is called before the first frame update
    void Start()
    {
        //CalcVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalcVolume(Mesh mesh)
    {
        m_mesh = mesh;

        CalcVolume();
    }

    public void CalcVolume()
    {
        StartCoroutine("Calculate");
    }

    public void ResetText()
    {
        if(m_text != null) m_text.text = "";
    }

    IEnumerator Calculate()
    {
        Debug.Log("Starting Volume Calculation!");
        Vector3[] positions = m_mesh.vertices;
        int[] indices = m_mesh.triangles;
        m_volume = 0;

        yield return null;

        int completeTris = Mathf.FloorToInt(indices.Length / 3.0f);
        for(int i = 0; i < completeTris; i++)
        {
            int startPos = i * 3;

            Vector3 edgeA = positions[indices[startPos + 2]] - positions[indices[startPos]];
            Vector3 edgeB = positions[indices[startPos + 1]] - positions[indices[startPos]];
            Vector3 edgeC = m_origin - positions[indices[startPos]];

            //Find out the absolute volume irrespective of direction
            float vol = Vector3.Dot(Vector3.Cross(edgeA, edgeB), edgeC) / 6.0f;
            m_volume += vol;

            if(m_text != null) m_text.text = "Vol: " + m_volume.ToString("#.00000");

            //yield return null;
        }

        yield return null;
    }
}
