using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(meshVert))]
public class meshVertEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        meshVert myVert = (meshVert)target;

        if(GUILayout.Button("Update Lines"))
        {
            myVert.UpdateLines(true);
        }

        if(GUILayout.Button("Add Next Vert"))
        {
            myVert.ConnectVertex(myVert.m_nextVert);
            myVert.m_nextVert.ConnectVertex(myVert);
            myVert.m_nextVert = null;
        }

        if(GUILayout.Button("Apply Force"))
        {
            myVert.CalcForceSplit(myVert.m_inputForce, myVert.m_hitForce);
        }

        if(GUILayout.Button("Apply Motion"))
        {
            myVert.ApplySquash(myVert.m_inputForce, 0.1f);
        }
    }
}
