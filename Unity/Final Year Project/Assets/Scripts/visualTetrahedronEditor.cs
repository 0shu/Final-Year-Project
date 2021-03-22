using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(visualTetrahedron))]
public class visualTetrahedronEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        visualTetrahedron myTetra = (visualTetrahedron)target;

        if(GUILayout.Button("Update Lines"))
        {
            myTetra.SetUpLines();
        }

        if(GUILayout.Button("Apply Hit"))
        {
            myTetra.ApplyHit1D();
        }
    }
}
